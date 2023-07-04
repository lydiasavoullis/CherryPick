==vera_morning==
~music = "morning ambient"
~characters +=vera
~currentSpeaker = "Vera"
¬
Good morning
{ChangeSprite("Vera", "vera_smile")}
I hope you have a nice day
{ChangeSprite("Vera", "vera_thinking")}
Remember I'm always here if you need any help
No shame in needing help
~currentSpeaker = "you"
Yeah ok
Thanks
~currentSpeaker = "Vera"
{ChangeSprite("Vera", "vera_excited")}
AH a customer is here
Good luck!
¬
~music = ""
~characters -=vera
->END

==vera_evening==
~music = "evening chill"
~characters +=vera
~currentSpeaker = "Vera"
¬
Good evening
{ChangeSprite("Vera", "vera_smile")}
I hope everything went well
{ChangeSprite("Vera", "vera_thinking")}
Was quite an eventful day huh?
~currentSpeaker = "you"
Yeah it went well I think
~currentSpeaker = "Vera"
{ChangeSprite("Vera", "vera_normal")}
Yes, I think so too
Remember to check the greenhouse before you leave
Goodnight!
¬
~music = ""
~characters -=vera
->END