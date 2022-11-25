// SPDX-License-Identifier: MIT
pragma solidity ^0.8.17;

// THE BEST GYM IN THE WEST SON! - https://www.youtube.com/watch?v=-2uU6OJV65w

contract ReinsGym { 


// Our example variables for this demo

uint public weight;
uint public stamina;


// All new users will sign up with these stats

function SignUp () public returns (bool)  {

weight = 65;
stamina = 10;

return true;

}


// fetch our current weight

function GetWeight () public view returns (uint) {
    return (weight);
}


// fetch our current stamina

function GetStamina () public view returns (uint) {
    return (stamina);
}


// We can use this function to adjust our stats in a positive way 

function IncreaseFitness (uint _weight, uint _stamina) public returns (bool) {
    weight = weight - _weight;
    stamina = stamina + _stamina;

    return true;
}


// We can use this function to adjust our stats in a negative way

function DecreaseFitness (uint _weight, uint _stamina) public returns (bool) {
    weight = weight + _weight;
    stamina = stamina - _stamina;

    return true;
}


// We can use this function to reset the demo

function ResetStats () public returns (bool) {
    weight = 0;
    stamina = 0;

    return true;
}

}