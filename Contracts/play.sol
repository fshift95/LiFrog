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
    return false;
   }else{
    players[userAddress].name = name;
    players[userAddress].time = block.timestamp;
    players[userAddress].isPlayerExist = true;
    return true;
   }
 
}


function setScore(address payable  userAddress,uint score) public returns(bool success) {
    scores[userAddress] = score;
    return true;
}


function sendBalance(address payable  userAddress,uint balanceToAdd) public returns(bool success) {
    balance[userAddress] = balanceToAdd;
    return true;
}


function getScore(address payable  userAddress) public view returns(uint score) {
      return  scores[userAddress] ;
}

function getPlayer(address payable  userAddress) public view returns(Player memory _player) {
      return  players[userAddress] ;
}

}