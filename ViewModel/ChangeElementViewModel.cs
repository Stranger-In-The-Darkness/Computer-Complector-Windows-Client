using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ViewModel.Delegates;
using ViewModel.Interfaces;

namespace ViewModel
{
	public class ChangeElementViewModel<T> : INotifyPropertyChanged, IModelEditor where T: ICloneable
	{
		private T _originalElement;
		private T _changedElement;

		private bool _changes = false;

		private RelayCommand _applyChanges;
		private RelayCommand _declineChanges;
		private RelayCommand _closeModelEdit;

		public T Element
		{
			get => _changedElement;
			set
			{
				_changedElement = value;
				if (!_originalElement.Equals(_changedElement))
				{
					_changes = true;
				}
				else
				{
					_changes = false;
				}
				OnPropertyChanged("Element");
			}
		}

		public bool Changes
		{
			get
			{
				return (_changes = !_originalElement.Equals(_changedElement));
			}
		}

		public RelayCommand ApplyChanges
		{
			get
			{
				return _applyChanges ??
					(_applyChanges = new RelayCommand(
						_ => 
						{
							Element = _changedElement;
							_originalElement = _changedElement;
						},
						_ => 
						{
							return Changes;
						}));
			}
		}

		public RelayCommand DeclineChanges
		{
			get
			{
				return _declineChanges ??
					(_declineChanges = new RelayCommand(
						_ =>
						{
							Element = (T)_originalElement.Clone();
						},
						_ =>
						{
							return Changes;
						}));
			}
		}

		public RelayCommand CloseModelEdit
		{
			get
			{
				return _closeModelEdit ??
					(_closeModelEdit = new RelayCommand(
						_ =>
						{
							if (Changes)
							{
								OnUnsavedChangesClose?.Invoke();
							}
							else
							{
								OnModelEditClosed?.Invoke();
							}
						},
						_ => true));
			}
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public event UnsavedChangesClose OnUnsavedChangesClose;

		public event ModelEditClosed OnModelEditClosed;

		public ChangeElementViewModel(T element)
		{
			_originalElement = element;
			_changedElement = (T)_originalElement.Clone();
		}

		public void OnPropertyChanged([CallerMemberName]string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
