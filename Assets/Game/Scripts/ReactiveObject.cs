#region References
using System.Collections.Generic;
using System.ComponentModel;
#endregion

public class ReactiveObject : INotifyPropertyChanged
{
	public event PropertyChangedEventHandler PropertyChanged = delegate { };
	protected bool SetProperty<T> (ref T field, T value)
	{
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged();
		return true;
	}
	protected bool SetProperty<T> (ref T field, T value, string propertyName)
	{
		if (EqualityComparer<T>.Default.Equals(field, value)) return false;
		field = value;
		OnPropertyChanged(propertyName);
		return true;
	}
	protected void OnPropertyChanged () { PropertyChanged.Invoke(this, null); }
	protected void OnPropertyChanged (string propertyName) { PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName)); }
}
