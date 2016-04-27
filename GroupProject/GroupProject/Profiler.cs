using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GroupProject
{
    class Profiler
    {
        #region Singleton
        private static Profiler _instance;

        /// <summary>
        /// Gets the one and only one instance
        /// of the profiler
        /// </summary>
        public static Profiler Instance
        {
            get
            {
                // Construct the object if necessary
                if (_instance == null)
                    _instance = new Profiler();

                // Return the one and only one instance
                // ever made
                return _instance;
            }
        }

        // Private constructor means
        // no one else can make one
        private Profiler() { }
        #endregion

        // Fields
        private Stopwatch _stopwatch;
        private LinkedList<double> _times;
        private int _maxDataPoints;
        private double _largestData;

        // Drawing related
        private Texture2D _pixel;
        private SpriteBatch _spriteBatch;

        // "Constants"
        private Color AxisColor = Color.White;
        private Color DataColor = Color.Red;
        private const int Height = 100;

        /// <summary>
        /// Initializes the profiler
        /// </summary>
        /// <param name="maxData">Max amount of data points to save</param>
        /// <param name="sb">The sprite batch for drawing</param>
        /// <param name="device">Graphics device for pixel creation</param>
        public void Initialize(int maxData, SpriteBatch sb, GraphicsDevice device)
        {
            // Save parameters
            _maxDataPoints = maxData;
            _spriteBatch = sb;

            // Set up other required data
            _stopwatch = new Stopwatch();
            _times = new LinkedList<double>();
            _largestData = 0;

            // Create and fill a simple 1x1 white texture
            _pixel = new Texture2D(device, 1, 1);
            _pixel.SetData<Color>(new Color[] { Color.White });
        }

        /// <summary>
        /// Starts the internal timer
        /// </summary>
        public void StartTimer()
        {
            _stopwatch.Start();
        }

        /// <summary>
        /// Stops the internal timer and
        /// records that data for drawing later
        /// </summary>
        public void StopTimer()
        {
            _stopwatch.Stop();

            // Add the time to the list
            double data = _stopwatch.Elapsed.TotalMilliseconds;
            _times.AddFirst(data);

            // Check for largest
            if (data > _largestData)
                _largestData = data;

            // Trim the list if necessary
            if (_times.Count > _maxDataPoints)
                _times.RemoveLast();

            // Reset stopwatch for future frames
            _stopwatch.Reset();
        }


        public void Draw(int x, int y)
        {
            // Draw the axes
            _spriteBatch.Draw(_pixel, new Rectangle(x, y, 1, Height), AxisColor);
            _spriteBatch.Draw(_pixel, new Rectangle(x, y + Height, _maxDataPoints, 1), AxisColor);

            // Calculate the scale of the data
            double heightScale = Height / _largestData;

            // Loop through the data and draw a pixel at a time
            int counter = 1;
            foreach (double data in _times)
            {
                _spriteBatch.Draw(
                    _pixel,
                    new Rectangle(
                        x + counter,
                        y + Height - (int)(data * heightScale),
                        1,
                        1),
                    DataColor);

                counter++;
            }
        }

    }
}
