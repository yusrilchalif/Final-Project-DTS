﻿using System;
using UnityEngine;

namespace Metalabs
{
	public struct Square
	{
		public Vector3 bottomLeft;
		public Vector3 bottomRight;
		public Vector3 topLeft;
		public Vector3 topRight;

		public Vector3 this[int index]
		{
			get
			{
				switch (index)
				{
					case 0:
						return this.bottomLeft;
					case 1:
						return this.bottomRight;
					case 2:
						return this.topLeft;
					case 3:
						return this.topRight;
					case 4:
						return this.bottomLeft; //so we wrap around back to start
					default:
						return Vector3.zero;
				}
			}
		}
	}
}