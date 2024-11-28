// SPDX-License-Identifier: GPL-3.0

pragma solidity >=0.7.0 <0.9.0;

contract play{

    address payable owner;

constructor() {
      owner=payable (msg.sender);
}


struct Player {
    string name; 
     uint time;
     bool isPlayerExist;
}






mapping (address => Player) players;
mapping (address => uint) scores;
mapping (address => uint) balance;

function addplayer(address payable  userAddress,string memory name) public returns(bool success) {
   
   if(players[userAddress].isPlayerExist){
    players[userAddress].name = name;
    return true;
   }else{
    players[userAddress].name = name;
    players[userAddress].time = block.timestamp;
    players[userAddress].isPlayerExist = true;
    return true;
   }
 
}


uint[3] highScore=[0,0,0];
address payable [3] highScoreAddres;

    

function getScore(address payable  userAddress) public view returns(uint score) {
      return  scores[userAddress] ;
}

    
    function setScore(address payable  userAddress,uint score)  external   {
       
        if(score>highScore[0]){
            highScore[2]=highScore[1];
            highScoreAddres[2]=highScoreAddres[1];
            highScore[1]=highScore[0];
             highScoreAddres[1]=highScoreAddres[0];
            highScore[0]=score;
             highScoreAddres[0]=payable (msg.sender);
        }else if(score>highScore[1]){
            highScore[2]=highScore[1];
             highScoreAddres[2]=highScoreAddres[1];
            highScore[1]=score;
                highScoreAddres[1]=payable (msg.sender);
        }else if(score>highScore[2]){
            highScore[2]=score;
             highScoreAddres[2]=payable (msg.sender);
        }     
        scores[userAddress] += score;
      
    }








function sendBalance(address payable  userAddress,uint balanceToAdd) public returns(bool success) {
    balance[userAddress] = balanceToAdd;
    return true;
}



function getPlayer(address payable  userAddress) public view returns(string memory name) {
      return  players[userAddress].name ;
}

}