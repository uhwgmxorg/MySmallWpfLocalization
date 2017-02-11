using GalaSoft.MvvmLight.Command;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Media;
using System.Threading;
using System.Windows.Data;

namespace MySmallWpfLocalization
{
    public enum Languages
    {
        German,
        English,
        Polish,
        Dutch,
        Italian,
        France
    }

    /// <summary>
    /// Interaktionslogik für MainWindow.xaml in the ViewModel
    /// </summary>
    public partial class MainWindowViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        // Radio-Buttons
        private Languages languages = Languages.German;
        public Languages Languages
        {
            get
            {
                return this.languages;
            }
            set
            {
                this.languages = value;
                OnPropertyChanged("Languages");
                OnPropertyChanged("IsGerman");
                OnPropertyChanged("IsEnglish");
                OnPropertyChanged("IsDutch");
                OnPropertyChanged("IsItalian");
                OnPropertyChanged("France");
            }
        }
        public bool IsGerman
        {
            get { return Languages == Languages.German; }
            set { Languages = value ? Languages.German : Languages; }
        }
        public bool IsEnglish
        {
            get { return Languages == Languages.English; }
            set { Languages = value ? Languages.English : Languages; }
        }
        public bool IsPolish
        {
            get { return Languages == Languages.Polish; }
            set { Languages = value ? Languages.Polish : Languages; }
        }
        public bool IsDutch
        {
            get { return Languages == Languages.Dutch; }
            set { Languages = value ? Languages.Dutch : Languages; }
        }
        public bool IsItalian
        {
            get { return Languages == Languages.Italian; }
            set { Languages = value ? Languages.Italian : Languages; }
        }
        public bool IsFrance
        {
            get { return Languages == Languages.France; }
            set { Languages = value ? Languages.France : Languages; }
        }

        #region OnPropertyChanged Properties
        private string lableLanguages;
        public string LableLanguages
        {
            get { return lableLanguages; }
            set
            {
                if (value != LableLanguages)
                {
                    lableLanguages = value;
                    OnPropertyChanged("LableLanguages");
                };
            }
        }
        private string headline;
        public string Headline
        {
            get { return headline; }
            set
            {
                if (value != Headline)
                {
                    headline = value;
                    OnPropertyChanged("Headline");
                };
            }
        }
        private string buttonCloseText;
        public string ButtonCloseText
        {
            get { return buttonCloseText; }
            set
            {
                if (value != ButtonCloseText)
                {
                    buttonCloseText = value;
                    OnPropertyChanged("ButtonCloseText");
                };
            }
        }
        private string buttonPlayText;
        public string ButtonPlayText
        {
            get { return buttonPlayText; }
            set
            {
                if (value != ButtonPlayText)
                {
                    buttonPlayText = value;
                    OnPropertyChanged("ButtonPlayText");
                };
            }
        }
        private string buttonStopText;
        public string ButtonStopText
        {
            get { return buttonStopText; }
            set
            {
                if (value != ButtonStopText)
                {
                    buttonStopText = value;
                    OnPropertyChanged("ButtonStopText");
                };
            }
        }

        private string buttonPlayToolTipText;
        public string ButtonPlayToolTipText
        {
            get { return buttonPlayToolTipText; }
            set
            {
                if (value != ButtonPlayToolTipText)
                {
                    buttonPlayToolTipText = value;
                    OnPropertyChanged("ButtonPlayToolTipText");
                };
            }
        }
        private string buttonStopToolTipText;
        public string ButtonStopToolTipText
        {
            get { return buttonStopToolTipText; }
            set
            {
                if (value != ButtonStopToolTipText)
                {
                    buttonStopToolTipText = value;
                    OnPropertyChanged("ButtonStopToolTipText");
                };
            }
        }

        private Image sDImageFlag;
        public Image SDImageFlag
        {
            get { return sDImageFlag; }
            set
            {
                if (value != SDImageFlag)
                {
                    sDImageFlag = value;
                    OnPropertyChanged("SDImageFlag");
                };
            }
        }
        #endregion

        SoundPlayer Splayer { get; set; }

        /// <summary>
        /// Commands
        /// <summary>
        public RelayCommand PlayCommand { get; private set; }
        public RelayCommand StopCommand { get; private set; }
        public RelayCommand CloseCommand { get; private set; }         

        public RelayCommand<object> LanguGerman { get; private set; }
        public RelayCommand<object> LanguEnglish { get; private set; }
        public RelayCommand<object> LanguPolish { get; private set; }
        public RelayCommand<object> LanguDutch { get; private set; }
        public RelayCommand<object> LanguItalian { get; private set; }
        public RelayCommand<object> LanguFrance { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindowViewModel()
        {
            PlayCommand = new RelayCommand(PlayCommandCF, CanPlayCommand);
            StopCommand = new RelayCommand(StopCommandCF, CanStopCommand);
            CloseCommand = new RelayCommand(CloseCommandCF, CanCloseCommand);

            LanguGerman = new RelayCommand<object>(LanguGermanToCommandF);
            LanguEnglish = new RelayCommand<object>(LanguEnglishToCommandF);
            LanguPolish = new RelayCommand<object>(LanguPolishToCommandF);
            LanguDutch = new RelayCommand<object>(LanguDutchToCommandF);
            LanguItalian = new RelayCommand<object>(LanguItalianToCommandF);
            LanguFrance = new RelayCommand<object>(LanguFranceToCommandF);

            LoadAllLanguDependentResources();
        }

        /******************************/
        /*      Button Functions      */
        /******************************/
        #region Button Command Functions

        /// <summary>
        /// PlayCommandCF
        /// </summary>
        private void PlayCommandCF()
        {
            Splayer.Play();
        }

        /// <summary>
        /// CanPlayCommand
        /// </summary>
        /// <returns></returns>
        private bool CanPlayCommand()
        {
            return true;
        }

        /// <summary>
        /// StopCommandCF
        /// </summary>
        private void StopCommandCF()
        {
            Splayer.Stop();
        }

        /// <summary>
        /// CanStopCommand
        /// </summary>
        /// <returns></returns>
        private bool CanStopCommand()
        {
            return true;
        }

        /// <summary>
        /// CloseCommandCF
        /// </summary>
        private void CloseCommandCF()
        {
            App.Current.Shutdown();
        }

        /// <summary>
        /// CanCloseCommand
        /// </summary>
        /// <returns></returns>
        private bool CanCloseCommand()
        {
            return true;
        }

        #endregion
        /******************************/
        /*     Command Functions      */
        /******************************/
        #region Command Functions

        /// <summary>
        /// LanguGermanToCommandF
        /// </summary>
        /// <param name="obj"></param>
        private void LanguGermanToCommandF(object obj)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("de");
            LoadAllLanguDependentResources();
        }

        /// <summary>
        /// LanguEnglishToCommandF
        /// </summary>
        /// <param name="obj"></param>
        private void LanguEnglishToCommandF(object obj)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            LoadAllLanguDependentResources();
        }

        /// <summary>
        /// LanguPolishToCommandF
        /// </summary>
        /// <param name="obj"></param>
        private void LanguPolishToCommandF(object obj)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("pl");
            LoadAllLanguDependentResources();
        }

        /// <summary>
        /// LanguDutchToCommandF
        /// </summary>
        /// <param name="obj"></param>
        private void LanguDutchToCommandF(object obj)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("nl");
            LoadAllLanguDependentResources();
        }

        /// <summary>
        /// LanguItalianToCommandF
        /// </summary>
        /// <param name="obj"></param>
        private void LanguItalianToCommandF(object obj)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("it");
            LoadAllLanguDependentResources();
        }

        /// <summary>
        /// LanguFranceToCommandF
        /// </summary>
        /// <param name="obj"></param>
        private void LanguFranceToCommandF(object obj)
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("fr");
            LoadAllLanguDependentResources();
        }

        #endregion
        /******************************/
        /*      Menu Events          */
        /******************************/
        #region Menu Events

        #endregion
        /******************************/
        /*      Other Functions       */
        /******************************/
        #region Other Functions

        /// <summary>
        /// LoadAllLanguDependentResources
        /// </summary>  
        private void LoadAllLanguDependentResources()
        {
            Headline = Properties.Resources.IDS_Headline;
            LableLanguages = Properties.Resources.IDS_Langu;

            ButtonCloseText = Properties.Resources.IDS_Close;
            ButtonPlayText = Properties.Resources.IDS_Play;
            ButtonStopText = Properties.Resources.IDS_Stop;

            ButtonPlayToolTipText = Properties.Resources.IDSTT_PlayToolTip;
            ButtonStopToolTipText = Properties.Resources.IDSTT_StopToolTip;

            SDImageFlag = Properties.Resources.IDPIC_FLAG;

            //  Stop playing the national anthem if user change the langu
            if (Splayer != null) Splayer.Stop();
            Splayer = new System.Media.SoundPlayer(Properties.Resources.IDWAV_NA);
        }

        /// <summary>
        /// OnPropertyChanged
        /// </summary>
        /// <param name="p"></param>
        private void OnPropertyChanged(string p)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(p));
        }

        #endregion
    }

    /// <summary>
    /// One-way converter from System.Drawing.Image to System.Windows.Media.ImageSource
    /// </summary>
    [ValueConversion(typeof(System.Drawing.Image), typeof(System.Windows.Media.ImageSource))]
    public class ImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            // empty images are empty...
            if (value == null) { return null; }

            var image = (System.Drawing.Image)value;
            // Winforms Image we want to get the WPF Image from...
            var bitmap = new System.Windows.Media.Imaging.BitmapImage();
            bitmap.BeginInit();
            System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
            // Save to a memory stream...
            image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
            // Rewind the stream...
            memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
            bitmap.StreamSource = memoryStream;
            bitmap.EndInit();
            return bitmap;
        }

        public object ConvertBack(object value, Type targetType,
            object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
