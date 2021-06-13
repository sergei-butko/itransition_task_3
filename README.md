# Description
Task #3 "Rock Paper Scissors" for the internship on .NET in Itransition.

The program is launched with an odd number >= 3 command line parameters 
(if the arguments are incorrectly specified, an error message display
what is wrong and an example how it is correct).
These parameters are moves (for example: Rock Paper Scissors or Rock Paper Scissors Lizard Spock).

Victory is determined in the following way - half of the next ones wins, half of the previous ones lose (in a circle).

The script generates a random key (RandomNumberGenerator - required) 128 bits long, 
makes its move, calculates the HMAC (based on SHA2 or SHA3) from the move with the generated key and shows it to the user.

After that, the user receives a "Menu":
1 - Stone
2 - Paper
.....
0 - Exit.

The user makes his choice (if the input is incorrect, the "Menu" is displayed again).

The script shows who won, the turn of the computer and the original key.
Thus, the user can check that the computer is playing fairly (did not change its move after the user's move).

Performed by Serhii Butko.
