// contracts/FRLToken.sol
// SPDX-License-Identifier: MIT
pragma solidity ^0.8.0;

import "@openzeppelin/contracts@v4.9.3/token/ERC20/ERC20.sol";

contract FRGToken is ERC20 {
   
    constructor() ERC20("Frogly", "FRG") {
        _mint(msg.sender, 10000000000000000000000);
    }
}

