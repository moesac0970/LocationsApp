using System;
using Xamarin.Forms;

namespace B4.PE3.VanLookManu.Behaviors
{
    /// <summary>
    /// all refs: https://docs.microsoft.com/nl-nl/xamarin/xamarin-forms/app-fundamentals/gestures/
    /// example:  https://github.com/howest-gp-mde/cu-xam-mvvmbasics
    /// </summary>
    public class BindableBehavior<T> : Behavior<T> where T : BindableObject
    {
        public T AssociatedObject { get; private set; }

        protected override void OnAttachedTo(T visualElement)
        {
            base.OnAttachedTo(visualElement);
            AssociatedObject = visualElement;

            if (visualElement.BindingContext != null)
                BindingContext = visualElement.BindingContext;

            visualElement.BindingContextChanged += OnBindingContextChanged;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            OnBindingContextChanged();
        }

        protected override void OnDetachingFrom(T view)
        {
            view.BindingContextChanged -= OnBindingContextChanged;
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            BindingContext = AssociatedObject.BindingContext;
        }
    }
}