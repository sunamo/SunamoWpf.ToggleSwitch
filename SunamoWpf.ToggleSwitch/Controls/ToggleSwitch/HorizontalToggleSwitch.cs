namespace SunamoWpf.Controls.ToggleSwitch;

	/// <summary>
	/// Represents the horizontal toggle switch type.
	/// </summary>
	public class HorizontalToggleSwitch : ToggleSwitchBase
	{
		/// <summary>
		/// Initializes a new instance of the horizontal toggle switch class.
		/// </summary>
		public HorizontalToggleSwitch()
		{
			DefaultStyleKey = typeof(HorizontalToggleSwitch);
		}

		/// <summary>
		/// The offset.
		/// </summary>
		protected override double Offset
		{
			get { return Canvas.GetLeft(SwitchThumb); }
			set
			{
#if WPF
				SwitchTrack.BeginAnimation(Canvas.LeftProperty, null);
				SwitchThumb.BeginAnimation(Canvas.LeftProperty, null);
#endif
				Canvas.SetLeft(SwitchTrack, value);
				Canvas.SetLeft(SwitchThumb, value);
			}
		}

		/// <summary>
		/// The slide property path.
		/// </summary>
		protected override PropertyPath SlidePropertyPath
		{
			get { return new PropertyPath("(Canvas.Left)"); }
		}

		/// <summary>
		/// On drag delta.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="eventArgs">The event args.</param>
		protected override void OnDragDelta(object sender, DragDeltaEventArgs eventArgs)
		{
#if SILVERLIGHT
			DragOffset += e.HorizontalChange * ZoomFactor;
#else
			DragOffset += eventArgs.HorizontalChange;
#endif
			Offset = Math.Max(UncheckedOffset, Math.Min(CheckedOffset, DragOffset));
		}

		/// <summary>
		/// Layout controls.
		/// </summary>
		protected override void LayoutControls()
		{
			if (SwitchThumb == null || SwitchRoot == null)
			{
				return;
			}

			double fullThumbWidth = SwitchThumb.ActualWidth + SwitchThumb.BorderThickness.Left + SwitchThumb.BorderThickness.Right;

			if (SwitchChecked != null && SwitchUnchecked != null)
			{
				SwitchChecked.Width = SwitchUnchecked.Width = Math.Max(0, SwitchRoot.ActualWidth - fullThumbWidth / 2);
				SwitchChecked.Padding = new Thickness(0, 0, (SwitchThumb.ActualWidth + SwitchThumb.BorderThickness.Left) / 2, 0);
				SwitchUnchecked.Padding = new Thickness((SwitchThumb.ActualWidth + SwitchThumb.BorderThickness.Right) / 2, 0, 0, 0);
			}

			SwitchThumb.Margin = new Thickness(SwitchRoot.ActualWidth - fullThumbWidth, SwitchThumb.Margin.Top, 0, SwitchThumb.Margin.Bottom);
			UncheckedOffset = -SwitchRoot.ActualWidth + fullThumbWidth - SwitchThumb.BorderThickness.Left;
			CheckedOffset = SwitchThumb.BorderThickness.Right;

			if (!IsDragging)
			{
				Offset = IsChecked ? CheckedOffset : UncheckedOffset;
				ChangeCheckStates(false);
			}
		}

		/// <summary>
		/// On drag completed.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="eventArgs">The event args.</param>
		protected override void OnDragCompleted(object sender, DragCompletedEventArgs eventArgs)
		{
			IsDragging = false;
			bool click = false;
			double fullThumbWidth = SwitchThumb.ActualWidth + SwitchThumb.BorderThickness.Left + SwitchThumb.BorderThickness.Right;

			if ((!IsChecked && DragOffset > (SwitchRoot.ActualWidth - fullThumbWidth) * (Elasticity - 1.0))
				 || (IsChecked && DragOffset < (SwitchRoot.ActualWidth - fullThumbWidth) * -Elasticity))
			{
				double edge = IsChecked ? CheckedOffset : UncheckedOffset;
				if (Offset != edge)
				{
					click = true;
				}
			}
			else if (DragOffset == CheckedOffset || DragOffset == UncheckedOffset)
			{
				click = true;
			}
			else
			{
				ChangeCheckStates(true);
			}

			if (click)
			{
				OnClick();
			}

			DragOffset = 0;
#if SILVERLIGHT
			ReleaseMouseCaptureInternal();
#endif
		}
	}