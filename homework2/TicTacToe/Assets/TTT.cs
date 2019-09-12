using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTT : MonoBehaviour {
	private int turn =1;//1 for Player1,0 for player2
	private int [,] board =new int[3,3];

//reset
	void reset(){
		turn = 1;
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				board [i, j] = 0;
			}
		}
	}
//judge result,4 for draw for the game
	int judge (){
		//Horizontal Direction Judgment
		for (int i = 0; i < 3; i++) {
			if(board[i,0] == board[i,1] && board[i,0] == board[i,2] && board[i,0] != 0){
				return board[i,0];
			}
		}
		//Vertical Direction Judgment
		for (int i = 0; i < 3; i++) {
			if (board[0,i] == board[1,i] && board[0,i] == board[2,i] && board[0,i] != 0 ){
				return board[0,i];
			}
		}
		//Diagonal Direction Judgment
		if(board[1,1]!=0 &&board[0,0]==board[1,1]&&board[1,1]==board[2,2]||board[2,0]==board[1,1]&&board[1,1]==board[0,2]){
			return board [1, 1];
		}
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if(board [i, j] == 0)
					return 0;
			}
		}
		return 3;
	}
	//start game,initialize
	void start (){
		reset ();
	}

	void OnGUI(){
		//reset game if player click button
		if (GUI.Button (new Rect (Screen.width/2-50,Screen.height/2+100,100,25), "start game")) {
			reset ();
		}

		//judge the result of the game while update each time
		int temp = judge();
		if(temp == 1)GUI.Label(new Rect (Screen.width/2-40,Screen.height/2-150,150,75),"<color=red><size=25>O wins</size></color>");
		if(temp == 2)GUI.Label(new Rect (Screen.width/2-40,Screen.height/2-150,150,75),"<color=red><size=25>X wins</size></color>");
		if(temp == 3)GUI.Label(new Rect (Screen.width/2-40,Screen.height/2-150,150,75),"<color=red><size=25>Draw</size></color>");

		//Place chessmen while update each time
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (board [i, j] == 1) {
					GUI.Button (new Rect (Screen.width / 2 - 75 + i * 50, Screen.height / 2 - 75 + j * 50, 50, 50), "O");
				} 
				else if (board [i, j] == 2) {
					GUI.Button (new Rect (Screen.width / 2 - 75 + i * 50, Screen.height / 2 - 75 + j * 50, 50, 50), "X");
				}
				else if (GUI.Button (new Rect (Screen.width / 2 - 75 + i * 50, Screen.height / 2 - 75 + j * 50, 50, 50), "")) {
					if(temp==0){
							if(turn==1){
								board[i,j] =1;
							}
							if(turn==-1){
								board[i,j]=2;
							}
							turn*=-1;
					}
				}
	    	}
		}
	}
}
