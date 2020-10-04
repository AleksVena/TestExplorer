using Lesson1.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Lesson1.ViewModels
{
    public class MainVM : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void onPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Disc selectedDisc;

        public Disc SelectedDisc
        {
            get => selectedDisc;
            set
            {
                selectedDisc = value;
                if (selectedDisc != null)
                {
                    if (selectedDisc.isReady)
                    {
                        selectedPaths = selectedDisc.name;
                    }
                    else
                        selectedPaths = null;

                    FillElements();

                }
            }
        }

        private void FillElements()
        {
            DirEnt = Element.GetElements(selectedPaths);
            onPropertyChanged("DirEnt");
        }

        private string selectedPaths { get; set; }

        private List<Element> dirEnt;

        public List<Element> DirEnt
        {
            get => dirEnt;
            set { 
                dirEnt = value;
                onPropertyChanged("PasteEnable");
            }
        }

        private int selectedIndexDisc;

        public int SelectedIndexDisc
        {
            get => selectedIndexDisc;
            set
            {
                selectedIndexDisc = value;
            }
        }

        public List<Disc> DrivesList
        {
            get => Disc.getDiscs();
        }

        private Element selectedObject;
        public Element SelectedObject
        {
            get => selectedObject;
            set
            {
                selectedObject = value;
                onPropertyChanged("CopyEnable");
                onPropertyChanged("DeleteEnable");
                onPropertyChanged("CutEnable");
                onPropertyChanged("openFolderEnable");
            }
        }

        public bool CopyEnable
        {
            get => selectedObject != null;
        }

        public bool DeleteEnable
        {
            get => selectedObject != null;
        }

        public bool CutEnable
        {
            get => selectedObject != null;
        }

        private List<Element> bufer = new List<Element>();

        public bool PasteEnable
        {
            get => bufer.Count > 0 && selectedPaths != null;
        }

        public bool openFolderEnable
        {
            get => selectedObject != null && selectedObject.isFolder;
        }

        public ICommand CopyClick
        {
            get => new DelegateCommand(executeAction => { addSelectedBuffer(false); }, canExecute => { return true; });
        }

        public ICommand CutClick
        {
            get => new DelegateCommand(executeAction => { addSelectedBuffer(true); }, canExecute => { return true; });
        }

        public ICommand PasteClick
        {
            get => new DelegateCommand(executeAction => { onPasteClick(); }, canExecute => { return true; });
        }

        public ICommand DeleteClick
        {
            get => new DelegateCommand(executeAction => { onDeleteClick(); }, canExecute => { return true; });
        }

        private void onDeleteClick()
        {
            StartAction.make(
                "delete",
                dirEnt.Where(x => x.IsSelected).ToList(),
                null
                );
            FillElements();
        }
        private void onPasteClick()
        {
            StartAction.make(
                IsCut ? "cut" : "copy",
                bufer,
                selectedPaths
                );
            bufer = new List<Element>();
            onPropertyChanged("PasteEnable");
            FillElements();
        }

        public bool IsCut { get; set; }

        public void addSelectedBuffer(bool isCut)
        {
            IsCut = isCut;
            bufer.AddRange(dirEnt.Where(x => x.IsSelected));
            onPropertyChanged("PasteEnable");
        }

        public ICommand CompareClick
        {
            get => new DelegateCommand(executeAction => { onCompareClick(); }, canExecute => { return true; });
        }

        public void onCompareClick()
        {
            if (DirEnt == null || DirEnt.Where(x => x.isFolder && x.IsSelected).Count() != 2)
            {
                MessageBox.Show("Select two directories!");
                return;
            }
        }

        public ICommand openFolder
        {
            get => new DelegateCommand(executeAction => { onOpenFolder(); }, canExecute => { return true; });
        }

        public void onOpenFolder()
        {
            if (selectedObject != null && selectedObject.isFolder)
            {
                selectedPaths = selectedObject.fullName;
                FillElements();
                selectedDisc = null;
                onPropertyChanged("SelectedDisc");
            }
        }
    }
}
