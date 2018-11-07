using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.ServiceModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using WindowsServiceHostTest;
using TaskResultTest.Annotations;

namespace TaskResultTest
{
   internal class WindowContext : IWindowContext
   {
      private string _buttonCaptionTaskFactoryStartNew;
      private string _buttonCaptionAsyncAwait;
      private string _threadId;
      private string _buttonCaptionTaskRun;
      private string _result;
      public event PropertyChangedEventHandler PropertyChanged;

      [NotifyPropertyChangedInvocator]
      protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
      {
         PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }

      public WindowContext()
      {
         ButtonCaptionTaskFactoryStartNew = "Task.Factory.StartNew()";
         ButtonCaptionAsyncAwait = "Async/Await";
         ButtonCaptionTaskRun = "Task.Run()";
         UpdateThreadId();
      }
      public ICommand OnClickTaskFactoryStartNew => new RelayCommand(ClickCommandTaskFactoryStartNew);
      public ICommand OnClickAsyncAwait => new RelayCommand(ClickCommandAsyncAwait);
      public ICommand OnClickTaskRun => new RelayCommand(ClickCommandTaskRun);
      public ICommand OnClear => new RelayCommand(ClickCommandClear);

    

      public string ButtonCaptionTaskFactoryStartNew
      {
         get { return _buttonCaptionTaskFactoryStartNew; }
         set { _buttonCaptionTaskFactoryStartNew = value; OnPropertyChanged(nameof(ButtonCaptionTaskFactoryStartNew)); }
      }
      public string ButtonCaptionAsyncAwait
      {
         get { return _buttonCaptionAsyncAwait; }
         set { _buttonCaptionAsyncAwait = value; OnPropertyChanged(nameof(ButtonCaptionAsyncAwait)); }
      }
      public string ButtonCaptionTaskRun
      {
         get { return _buttonCaptionTaskRun; }
         set { _buttonCaptionTaskRun = value; OnPropertyChanged(nameof(ButtonCaptionTaskRun)); }
      }
      public string ThreadId
      {
         get { return _threadId; }
         set { _threadId = value; OnPropertyChanged(nameof(ThreadId)); }
      }
      public string Result
      {
         get { return _result; }
         set { _result = value; OnPropertyChanged(nameof(Result)); }
      }

      public async void ClickCommandTaskFactoryStartNew()
      {
         ButtonCaptionTaskFactoryStartNew = "Running";
         var res = await Task.Factory.StartNew(() => CallService(nameof(ButtonCaptionTaskFactoryStartNew)));
         lock (this)
         {
            UpdateResult(res);
         }
         ButtonCaptionTaskFactoryStartNew = "Task.Factory.StartNew()";
      }

      private async void ClickCommandAsyncAwait()
      {
         ButtonCaptionAsyncAwait = "Running";
         var res = await CallServiceAsync(nameof(ButtonCaptionAsyncAwait));
         lock (this)
         {
            UpdateResult(res);
         }
         ButtonCaptionAsyncAwait = "Async/Await";
      }

      private async void ClickCommandTaskRun()
      {
         ButtonCaptionTaskRun = "Running";
         var res = await Task.Run(() => CallService(nameof(ButtonCaptionTaskRun)));
         lock (this)
         {
            UpdateResult(res);
         }
         ButtonCaptionTaskRun = "Task.Run()";
      }

      private void ClickCommandClear()
      {
         lock (this)
         {
            Result = "";
         }
      }

      private void UpdateThreadId()
      {
         ThreadId = $"Thread Id: {Thread.CurrentThread.ManagedThreadId.ToString()}";
      }

      private void UpdateResult(string res)
      {
         UpdateThreadId();
         Result += res + "\n";
      }
     
      private string CallService(string name)
      {
         string res;
         UpdateThreadId();
         var binding = new BasicHttpBinding();
         var endpoint = new EndpointAddress("http://localhost:8080/hello");

         using (var channelFactory = new ChannelFactory<IHelloWorldService>(binding, endpoint))
         {
            try
            {
               var client = channelFactory.CreateChannel();
               res = client.SayHello(name);
               channelFactory.Close();
            }
            catch (Exception e)
            {
               channelFactory.Abort();
               throw;
            }
         }
         return res;
      }
      private async Task<string> CallServiceAsync(string name)
      {
         string res;

         UpdateThreadId();
         var binding = new BasicHttpBinding();
         var endpoint = new EndpointAddress("http://localhost:8080/hello");

         using (var channelFactory = new ChannelFactory<IHelloWorldService>(binding, endpoint))
         {
            try
            {
               var client = channelFactory.CreateChannel();
               res = await client.SayHelloWithTask(name);
               channelFactory.Close();
            }
            catch (Exception e)
            {
               channelFactory.Abort();
               throw;
            }
         }
         return res;
      }

      private static long DoSomething()
      {
         var lastTicks = DateTime.Now.Ticks;
         Parallel.ForEach(
            new Infinity(),
            new ParallelOptions { MaxDegreeOfParallelism = 100 },
            i => { lastTicks = (int)DateTime.Now.Ticks * (int)lastTicks / 98374 + 187263 - 19862 % 5 * 298374; });
         return lastTicks;
      }

   }
}
