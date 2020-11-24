using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace ImageProcessingApp
{
    /// <summary>
    /// A base view model that fires Property Changed event as needed
    /// </summary>
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// The event that is fired when any child property changes its value
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/> event
        /// </summary>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = "")
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        /// <summary>
        /// Call this to fire a <see cref="PropertyChanged"/> event with lambda
        /// </summary>
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> property)
        {
            string propertyName = ((MemberExpression)property.Body).Member.Name;
            OnPropertyChanged(propertyName);
        }
    }
}
