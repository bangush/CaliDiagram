﻿using System.Windows;
using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiagramLib.ViewModels
{

    public abstract class DiagramBaseViewModel : PropertyChangedBase
    {
        public DiagramBaseViewModel()
        {
            AttachPlacement = new AttachDescriptorPlacement(this);
        }
        public event EventHandler LocationChanged;
        public event EventHandler SizeChanged;
        public event EventHandler BindingComplete;

        protected virtual void OnLocationChanged()
        {
        }

        public object BelongsTo
        {
            get; set; 
            
        }

        protected virtual void OnSizeChanged()
        {

        }
        protected virtual void OnBindingCompleted()
        {

        }

        public Rect Rect
        {
            get
            {
                return new Rect(X, Y, Width, Height);
            }
        }

        void RaiseLocationChanged()
        {
            // AttachPlacement.UpdatePoints();
            OnLocationChanged();
            if (LocationChanged != null)
                LocationChanged(this, null);
        }

        public void RaiseBindingComplete()
        {
            OnBindingCompleted();
            // AttachPlacement.UpdatePoints();
    
            //   Width = view.ActualWidth;
            //   Height = view.ActualHeight;

            if (BindingComplete != null)
                BindingComplete(this, null);
        }

        public void RaiseSizeChanged()
        {

            //  AttachPlacement.UpdatePoints();
            OnSizeChanged();
            if (SizeChanged != null)
                SizeChanged(this, null);
        }

        public bool HaveSize { get; private set; }

        public void SetLocation(double x, double y)
        {
            if (X == x && Y == y)
                return;
            this.X = x;
            this.Y = y;

            RaiseLocationChanged();
        }

        public void UpdateAttachPoints()
        {
            AttachPlacement.UpdatePoints();
        }

        public void SetLocation(Point point)
        {
            SetLocation(point.X, point.Y);
        }

        double _x;
        public double X
        {
            get { return _x; }
            set
            {
                if (_x != value)
                {
                    _x = value;
                    NotifyOfPropertyChange(() => X);
                }
            }
        }

        double _y;
        public double Y
        {
            get { return _y; }
            set
            {
                if (_y != value)
                {
                    _y = value;
                    NotifyOfPropertyChange(() => Y);
                }
            }
        }


        private double _Width;
        public double Width
        {
            get { return _Width; }
            set
            {
                if (_Width != value)
                {
                    _Width = value;
                    NotifyOfPropertyChange(() => Width);
                    RaiseSizeChanged();
                }
            }
        }

        private double _Height;
        public double Height
        {
            get { return _Height; }
            set
            {
                if (_Height != value)
                {
                    _Height = value;
                    NotifyOfPropertyChange(() => Height);
                    RaiseSizeChanged();
                    HaveSize = true;
                }
            }
        }


        public AttachDescriptorPlacement AttachPlacement
        {
            get; set; }

        public AttachPoint Attach(DiagramBaseViewModel controlToAttach, AttachDirection direction)
        {
            return AttachPlacement.Attach(controlToAttach, direction);
        }

        public void Detach(AttachPoint ap)
        {
            AttachPlacement.Detach(ap);
        }


        public event EventHandler<ViewAttachedEventArgs> ViewAttached;
    }
}