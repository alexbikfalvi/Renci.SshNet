/* 
 * Copyright (C) 2013 Alex Bikfalvi
 *
 * This program is free software; you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation; either version 3 of the License, or (at
 * your option) any later version.
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
 * General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with this program; if not, write to the Free Software
 * Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA 02110-1301, USA.
 */

using System;
using System.IO;
using System.Text;

namespace Renci.SshNet.Common
{
	/// <summary>
	/// A reader used for reading from a pipe stream.
	/// </summary>
	public class PipeReader : TextReader
	{
		private PipeStream _stream;
		private const int _bufferSize = 0x10000;
		private byte[] _buffer;
		private Encoding _encoding = Encoding.UTF8;

		/// <summary>
		/// Creates a new pipe reader instance for the specified stream.
		/// </summary>
		/// <param name="stream"></param>
		public PipeReader(PipeStream stream)
		{
			if (null == stream) throw new ArgumentNullException("stream");

			// Set the stream.
			this._stream = stream;
			// Allocate the buffer.
			this._buffer = new byte[PipeReader._bufferSize];
		}

		// Public properties.

		/// <summary>
		/// Gets or sets the current encoding.
		/// </summary>
		public Encoding Encoding
		{
			get { return this._encoding; }
			set { this._encoding = value; }
		}

		// Public methods.

		/// <summary>
		/// Closes the pipe reader and releases any system resources associated with the pipe reader.
		/// </summary>
		public override void Close()
		{
			base.Close();
		}

		/// <summary>
		/// Reads the next character without changing the state of the reader or the character source.
		/// Returns the next available character without actually reading it from the input stream.
		/// </summary>
		/// <returns>An integer representing the next character to be read, or -1 if no more characters are available or the stream does not support seeking.</returns>
		public override int Peek()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Reads the next character from the input stream and advances the character position by one character.
		/// </summary>
		/// <returns>The next character from the input stream, or -1 if no more characters are available. The default implementation returns -1.</returns>
		public override int Read()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Reads a maximum of count characters from the current stream and writes the data to buffer, beginning at index.
		/// </summary>
		/// <param name="buffer">When this method returns, contains the specified character array with the values between index and (index + count - 1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in buffer at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read. If the end of the stream is reached before count of characters is read into buffer, the current method returns.</param>
		/// <returns>The number of characters that have been read. The number will be less than or equal to count, depending on whether the data is available within the stream. This method returns zero if called when no more characters are left to read.</returns>
		public override int Read(char[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Reads a maximum of count characters from the current stream, and writes the data to buffer, beginning at index.
		/// </summary>
		/// <param name="buffer">When this method returns, this parameter contains the specified character array with the values between index and (index + count -1) replaced by the characters read from the current source.</param>
		/// <param name="index">The position in buffer at which to begin writing.</param>
		/// <param name="count">The maximum number of characters to read.</param>
		/// <returns>The position of the underlying stream is advanced by the number of characters that were read into buffer. The number of characters that have been read. The number will be less than or equal to count, depending on whether all input characters have been read.</returns>
		public override int ReadBlock(char[] buffer, int index, int count)
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Reads a line of characters from the current stream and returns the data as a string.
		/// </summary>
		/// <returns>The next line from the input stream, or null if all characters have been read.</returns>
		public override string ReadLine()
		{
			throw new NotImplementedException();
		}

		/// <summary>
		/// Reads all characters from the current position to the end of the pipe reader and returns them as one string.
		/// </summary>
		/// <returns>A string containing all characters from the current position to the end of the pipe reader.</returns>
		public override string ReadToEnd()
		{
			// Create a new read buffer.
			//byte[] readBuffer = new byte[IoUtils.bufferSize];
			// The output buffer.
			//byte[] outputBuffer = null;

			// The reading buffer index.
			int bufferIndex = 0;
			// The number of bytes read.
			int bytesRead;

			// Continue reading from the stream while the stream has some data.
			do
			{
				// If the free buffer capacity reaches zero.
				if (this._buffer.Length == bufferIndex)
				{
					// Increase the size of the buffer.
					Array.Resize<byte>(ref this._buffer, this._buffer.Length + PipeReader._bufferSize);
				}
				// Try to read from the stream to the buffer until the buffer capacity.
				if ((bytesRead = this._stream.Read(this._buffer, bufferIndex, this._buffer.Length - bufferIndex)) > 0)
				{
					// If the reading was successful, increment the buffer index with the number of bytes read.
					bufferIndex += bytesRead;
				}
			}
			while (this._stream.HasData);

			// Convert the buffer data to a string using the current encoding.
			return this._encoding.GetString(this._buffer, 0, bufferIndex);
		}

		// Protected methods.

		/// <summary>
		/// Disposes the current object.
		/// </summary>
		/// <param name="disposing">If <b>true</b>, clean both managed and native resources. If <b>false</b>, clean only native resources.</param>
		protected override void Dispose(bool disposing)
		{
			// Call the base class method.
			base.Dispose(disposing);
		}
	}
}
