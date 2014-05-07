namespace Codealike.WP8.Controls
{
    using System;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Media;

    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Controls.Primitives;

    public class SmartPanorama : Panorama
    {
        private PanningLayer _backgroundLayer;
        private int _frameCount;
        private int? _initialIndex;
        private PanningLayer _itemsLayer;
        private PanningLayer _titleLayer;

        public SmartPanorama()
        {
            CompositionTarget.Rendering += OnRendering;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _backgroundLayer = GetTemplateChild("BackgroundLayer") as PanningLayer;
            _titleLayer = GetTemplateChild("TitleLayer") as PanningLayer;
            _itemsLayer = GetTemplateChild("ItemsLayer") as PanningLayer;
        }

        public void Select(int itemIndex)
        {
            if ( itemIndex >= 0 && itemIndex <= Items.Count )
            {
                if ( Items.Count > 0 )
                {
                    Debug.WriteLine("Back:{0}", _backgroundLayer.ActualWidth);
                    Debug.WriteLine("Tit:{0}", _titleLayer.ActualWidth);
                    Debug.WriteLine("Item:{0}", _itemsLayer.ActualWidth);

                    //Fooling SelectedItem being read-only :-)
                    var item = Items[itemIndex] as PanoramaItem;
                    SetValue(SelectedItemProperty, item);

                    var panoramaItem = Items[0] as PanoramaItem;
                    if ( panoramaItem != null )
                    {
                        double itemWidth = panoramaItem.ActualWidth;
                        double itemsOffset = itemIndex == Items.Count - 1 ? itemWidth : -itemWidth * itemIndex;
                        double layerOffset;
                        if ( _itemsLayer.ActualWidth < _backgroundLayer.ActualWidth )
                            layerOffset = itemIndex == Items.Count - 1
                                              ? _backgroundLayer.ActualWidth - itemWidth
                                              : -itemWidth * itemIndex;
                        else
                            layerOffset = -itemWidth * itemIndex;
                        if ( _backgroundLayer != null )
                            _backgroundLayer.GoTo((int)layerOffset, TimeSpan.FromSeconds(0));
                        if ( _titleLayer != null )
                            _titleLayer.GoTo((int)layerOffset, TimeSpan.FromSeconds(0));
                        if ( _itemsLayer != null )
                            _itemsLayer.GoTo((int)itemsOffset, TimeSpan.FromSeconds(0));
                    }
                }
            }
        }

        /// <summary>
        ///     Allows panorama to complete initial animation before setting optional initial item
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event argument</param>
        private void OnRendering(object sender, EventArgs e)
        {
            if ( _frameCount++ == 3 )
            {
                CompositionTarget.Rendering -= OnRendering;
                if ( _initialIndex != null )
                {
                    Select(_initialIndex.Value);
                    _initialIndex = null;
                }
            }
        }

        #region InitialIndex

        /// <summary>
        ///     InitialIndex Dependency Property
        /// </summary>
        public static readonly DependencyProperty InitialIndexProperty =
            DependencyProperty.Register("InitialIndex", typeof(int), typeof(SmartPanorama),
                                        new PropertyMetadata(0, OnInitialIndexChanged));

        /// <summary>
        ///     Gets or sets the InitialIndex property. This dependency property
        ///     indicates ....
        /// </summary>
        public int InitialIndex
        {
            get { return (int)GetValue(InitialIndexProperty); }
            set { SetValue(InitialIndexProperty, value); }
        }

        /// <summary>
        ///     Handles changes to the InitialIndex property.
        /// </summary>
        private static void OnInitialIndexChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var target = (SmartPanorama)d;
            var oldValue = (int)e.OldValue;
            int newValue = target.InitialIndex;
            target.OnInitialIndexChanged(oldValue, newValue);
        }

        /// <summary>
        ///     Provides derived classes an opportunity to handle changes to the InitialIndex property.
        /// </summary>
        protected virtual void OnInitialIndexChanged(int oldValue, int newValue)
        {
            if ( _backgroundLayer != null && _titleLayer != null && _itemsLayer != null )
            {
                Select(newValue);
            }
            else
            {
                _initialIndex = newValue;
            }
        }

        #endregion
    }
}