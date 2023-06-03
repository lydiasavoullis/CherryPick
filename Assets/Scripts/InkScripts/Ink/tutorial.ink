==tutorial_vera==
~characters+=vera
 ~currentSpeaker = "you"
 ¬
//Uh hi
~currentSpeaker = "Vera"
Hi, my name's Vera. I'm your assistant
 ~currentSpeaker = "you"
 Nice to meet you Vera, I've heard about you
{ChangeSprite("Vera", "vera_shocked")}
~currentSpeaker = "Vera"
It wasn't from the news, was it?
~currentSpeaker = "you"
Uh, no. It was from Emily, the previous owner
{ChangeSprite("Vera", "vera_smile")}
~currentSpeaker = "Vera"
Ah OK, hehe she was so nice
 ~currentSpeaker = "you"
Have you been on the news recently?
{ChangeSprite("Vera", "vera_normal")}
~currentSpeaker = "Vera"
No
~currentSpeaker = "you"
Are you sure?
{ChangeSprite("Vera", "vera_smile")}
~currentSpeaker = "Vera"
Nope. Not at all. Have a nice day. Bye.
~currentSpeaker = "you"
...
~characters-=vera
->yulia_repeatable_1

-> END