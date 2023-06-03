==yulia_repeatable_1==
~characters+=yulia
 ~currentSpeaker = "you"
 ¬
//Uh hi
~currentSpeaker = "Yulia"
// Hey 
//~gift = "5,colour:Rr,height:tt,petals:five,clusters:CC,split:ss"
I would like two tall plants with red flowers
~task = "Yulia,1,3,height:tall"
~currentSpeaker = "you"
Ok, thanks for putting in that order
~characters-=yulia
// ~currentSpeaker = "you"
// Ok
// they're gone
// great
~end_of_day = "true"
~currentSpeaker = "you"
// It's the end of the day
// I can buy something new
~shop_state = "open"
//And maybe work on a few orders before I go to bed
-> END

==charlie_repeatable_1==
~end_of_day = "false"
~currentSpeaker = "you"
Wow
It's a new day
time for some new work
~currentSpeaker = "Charlie"
~characters+=charlie
¬
Hey
Over here
I want a short white flower please
~task = "Charlie,1,3,height:short"
~characters-=charlie
~end_of_day = "true"
~currentSpeaker = "you"
It's the end of the day
I can buy something new
~shop_state = "open"
And maybe work on a few orders before I go to bed
->END
