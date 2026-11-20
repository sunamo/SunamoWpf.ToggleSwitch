namespace SunamoWpf.Controls.ToggleSwitch.Utils;

	/// <summary>
	/// Represents the actual size property proxy type.
	/// </summary>
	public class ActualSizePropertyProxy : FrameworkElement, INotifyPropertyChanged
	{
		/// <summary>
		/// The property changed.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		#region ElementProperty (Dependancy Property)

		/// <summary>
		/// The element property.
		/// </summary>
		public static readonly DependencyProperty ElementProperty =
			DependencyProperty.Register("Element", typeof(FrameworkElement), typeof(ActualSizePropertyProxy),
										new PropertyMetadata(null, OnElementPropertyChanged));

		private static void OnElementPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
		{
        if (dependencyObject != null)
        {
            ((ActualSizePropertyProxy)dependencyObject).OnElementChanged(eventArgs);
        }
		}

		/// <summary>
		/// The element.
		/// </summary>
		public FrameworkElement Element
		{
			get { return (FrameworkElement)GetValue(ElementProperty); }
			set { SetValue(ElementProperty, value); }
		}

		#endregion

		/// <summary>
		/// The actual height value.
		/// </summary>
		public double ActualHeightValue
		{
			get { return Element == null ? 0 : Element.ActualHeight; }
		}

		/// <summary>
		/// The actual width value.
		/// </summary>
		public double ActualWidthValue
		{
			get { return Element == null ? 0 : Element.ActualWidth; }
		}

		private void OnElementChanged(DependencyPropertyChangedEventArgs eventArgs)
		{
			var oldElement = (FrameworkElement)eventArgs.OldValue;
			var newElement = (FrameworkElement)eventArgs.NewValue;

			if (oldElement != null)
			{
				oldElement.SizeChanged -= ElementSizeChanged;
			}

        if (newElement != null)
        {
            newElement.SizeChanged += ElementSizeChanged;
        }

			NotifyPropertyChanged();
		}

		private void ElementSizeChanged(object sender, SizeChangedEventArgs eventArgs)
		{
			NotifyPropertyChanged();
		}

		private void NotifyPropertyChanged()
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs("ActualWidthValue"));
				PropertyChanged(this, new PropertyChangedEventArgs("ActualHeightValue"));
			}
		}
	}