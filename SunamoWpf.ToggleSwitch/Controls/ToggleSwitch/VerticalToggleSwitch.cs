namespace SunamoWpf.Controls.ToggleSwitch;

	/// <summary>
	/// Represents the vertical toggle switch type.
	/// </summary>
	public class VerticalToggleSwitch : ToggleSwitchBase
	{
		/// <summary>
		/// Initializes a new instance of the vertical toggle switch class.
		/// </summary>
		public VerticalToggleSwitch()
		{
			DefaultStyleKey = typeof(VerticalToggleSwitch);
		}

		/// <summary>
		/// The offset.
		/// </summary>
		protected override double Offset
		{
			get { return Canvas.GetTop(SwitchThumb); }
			set
			{
#if WPF
				SwitchTrack.BeginAnimation(Canvas.TopProperty, null);
				SwitchThumb.BeginAnimation(Canvas.TopProperty, null);
#endif
				Canvas.SetTop(SwitchThumb, value);
				Canvas.SetTop(SwitchTrack, value);
			}
		}

		/// <summary>
		/// The slide property path.
		/// </summary>
		protected override PropertyPath SlidePropertyPath
		{
			get { return new PropertyPath("(Canvas.Top)"); }
		}

		/// <summary>
		/// On drag delta.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="eventArgs">The event args.</param>
		protected override void OnDragDelta(object sender, DragDeltaEventArgs eventArgs)
		{
#if SILVERLIGHT
			DragOffset += e.VerticalChange * ZoomFactor;
#else
			DragOffset += eventArgs.VerticalChange;
#endif
			Offset = Math.Min(UncheckedOffset, Math.Max(CheckedOffset, DragOffset));
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

			double fullThumbHeight = SwitchThumb.ActualHeight + SwitchThumb.BorderThickness.Top + SwitchThumb.BorderThickness.Bottom;

			if (SwitchChecked != null && SwitchUnchecked != null)
			{
				SwitchChecked.Height = SwitchUnchecked.Height = Math.Max(0, SwitchRoot.ActualHeight - fullThumbHeight / 2);
				SwitchChecked.Padding = new Thickness(0, 0, 0, (SwitchThumb.ActualHeight + +SwitchThumb.BorderThickness.Bottom) / 2);
				SwitchUnchecked.Padding = new Thickness(0, (SwitchThumb.ActualHeight + +SwitchThumb.BorderThickness.Top) / 2, 0, 0);
			}

			UncheckedOffset = SwitchRoot.ActualHeight - SwitchThumb.ActualHeight - SwitchThumb.Margin.Top - SwitchThumb.Margin.Bottom;
			CheckedOffset = 0;

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

			if ((!IsChecked && DragOffset < ((SwitchRoot.ActualHeight - SwitchThumb.ActualHeight) * (1.0 - Elasticity)))
				 || (IsChecked && DragOffset > ((SwitchRoot.ActualHeight - SwitchThumb.ActualHeight) * Elasticity)))
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