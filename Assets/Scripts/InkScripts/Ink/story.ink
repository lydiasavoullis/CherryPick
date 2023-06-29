INCLUDE mei_main_story.ink
INCLUDE yulia_main_story.ink
INCLUDE variables.ink
INCLUDE category_1.ink
INCLUDE tutorial.ink
EXTERNAL ChangeSprite(name,sprite)
EXTERNAL RemoveCharacter(name)
EXTERNAL AddCharacter(name,sprite)
EXTERNAL AddToUpcomingEvents(knotName)
EXTERNAL RemoveFromUpcomingEvents(knotName)
//->tutorial_pt1
->day_0


==day_0==
//->tutorial_vera
 ~characters+=yulia
 ~currentSpeaker = "you"
 ¬
//Uh hi
~currentSpeaker = "Yulia"
// Hey 
//~gift = "5,colour:Rr,height:tt,petals:five,clusters:CC,split:ss"
I would like two tall plants with red flowers
~task = "Yulia,1,3,colour:red,height:tall, petals:6"

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


==day_1==
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
~task = "Charlie,1,3,colour:white,height:short"
~characters-=charlie
~end_of_day = "true"
~currentSpeaker = "you"
It's the end of the day
I can buy something new
~shop_state = "open"
And maybe work on a few orders before I go to bed
->END
==day_2==
~shop_state = "closed"
~end_of_day = "false"
Wow
It's another day
time for some new work
~currentSpeaker = "Yulia"
 ~characters+=yulia
¬
Hey 
Sorry to bother you
I would like a tall plant with pink flowers
~task = "Yulia,1,3,colour:pink,height:tall"
~currentSpeaker = "you"
Ok, thanks for putting in that order
~characters-=yulia
~end_of_day = "true"
Oh look
It's the end of the day
I can buy something new
~shop_state = "open"
And maybe work on a few orders before I go to bed
->END
==day_3==
~end_of_day = "false"
Wow
It's another day
time for some new work
~currentSpeaker = "Charlie"
~characters+=charlie
¬
Hey
Over here
I want a short pink flower please
~task = "Charlie,1,2,colour:pink,height:short"
~characters-=charlie
~end_of_day = "true"
~currentSpeaker = "you"
Oh look
It's the end of the day
I can buy something new
~shop_state = "open"
And maybe work on a few orders before I go to bed
->END
==day_4==
~end_of_day = "false"
Wow
It's another day
time for some new work
 ~characters+=yulia
¬
Hey 
Sorry to bother you
I would like a tall plant with pink flowers
~task = "Yulia,1,3,colour:pink,height:tall"
~currentSpeaker = "you"
Ok, thanks for putting in that order
~characters-=yulia
~end_of_day = "true"
Oh look
It's the end of the day
I can buy something new
~shop_state = "open"
And maybe work on a few orders before I go to bed
->END
==day_5==
~end_of_day = "false"
Wow
It's another day
time for some new work
~currentSpeaker = "Charlie"
~characters+=charlie
¬
Hey
Over here
I want a short pink flower please
~task = "Charlie,1,2,colour:pink,height:short"
~characters-=charlie
~end_of_day = "true"
~currentSpeaker = "you"
Oh look
It's the end of the day
I can buy something new
~shop_state = "open"
And maybe work on a few orders before I go to bed
->END