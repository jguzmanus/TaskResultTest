using System.ComponentModel;
using System.Windows.Input;

namespace TaskResultTest
{
   public interface IWindowContext : INotifyPropertyChanged
   {
      ICommand OnClickTaskFactoryStartNew { get; }
      ICommand OnClickAsyncAwait { get; }
      ICommand OnClickTaskRun { get; }
      ICommand OnClear { get; }


      string ButtonCaptionTaskFactoryStartNew { get; set; }
      string ButtonCaptionAsyncAwait { get; set; }
      string ButtonCaptionTaskRun { get; set; }
      string ThreadId { get; set; }

      string Result { get; set; }

   }
}