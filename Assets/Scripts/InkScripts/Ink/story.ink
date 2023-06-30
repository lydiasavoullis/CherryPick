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
 ~characters+=vera
 ~characters+=mei
 ¬
//  ~currentSpeaker = "Vera"
// Hello Yulia
 ~currentSpeaker = "Yulia"
// Hi dear
// ~currentSpeaker = "Vera"
// {ChangeSprite("Vera", "vera_smile")}
// How are you today?
// ~currentSpeaker = "Yulia"
// {ChangeSprite("Yulia", "yulia_sleeping")}
// ...
// Tired
// {ChangeSprite("Yulia", "yulia_normal")}
Can I have a red flower?
~task = "Yulia,1,3,colour:red"
~currentSpeaker = "you"
Ok, thanks for putting in that order
// And what would you like miss?
// ~currentSpeaker = "Mei"
// {ChangeSprite("Vera", "vera_thinking")}
// Excuse me 
// I work in the shop next door
// ~currentSpeaker = "you"
// The cafe?
// ~currentSpeaker = "Mei"
// {ChangeSprite("Mei", "mei_annoyed")}
// Yes
// That one
// It has come to my attention
// That you are the new manager of this shop
// ~currentSpeaker = "you"
// Yes?
// ~currentSpeaker = "Mei"
// {ChangeSprite("Mei", "mei_angry")}
// Well I know what you're doing
// Using toxic chemicals to grow those plants
// ~currentSpeaker = "you"
// What? No, we don't use chemicals
// ~currentSpeaker = "Mei"
// {ChangeSprite("Mei", "mei_annoyed")}
// I'm going to report you to the authorities
// Then you'll be sorry
// ~currentSpeaker = "you"
// If you are really suspicious that we are doing that here
// Why come and tell me what you are going to do?
// Just go and report me
// They will do an investigation and that will sort it out
// ~currentSpeaker = "Mei"
// Investi...
// {ChangeSprite("Mei", "mei_normal")}
// Ok whatever nevermind
// Forget I ever said anything
// ¬
// ~characters-=mei
// ~currentSpeaker = "you"
// What was THAT?
//  ~currentSpeaker = "Vera"
//  {ChangeSprite("Vera", "vera_normal")}
//  Oh that's just Mei
//  She works in the cafe next door
//  Don't worry about her
 ¬
 ~characters-=vera
~characters-=yulia
~characters-=mei
~end_of_day = "true"
~currentSpeaker = "you"
~shop_state = "open"
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