using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Ship {
	public bool isCurrentPlayer;

	override public void FixedUpdate()
	{
		if(isCurrentPlayer)
			base.FixedUpdate();
	}
}
