INCLUDE mei_main_story.ink
INCLUDE oliver_main_story.ink
INCLUDE yulia_main_story.ink
INCLUDE variables.ink
INCLUDE category_1.ink
INCLUDE tutorial.ink
INCLUDE morning_events.ink
EXTERNAL ChangeSprite(name,sprite)
EXTERNAL RemoveCharacter(name)
EXTERNAL AddCharacter(name,sprite)
EXTERNAL AddToUpcomingEvents(knotName)
EXTERNAL RemoveFromUpcomingEvents(knotName)
//->tutorial_pt1
->day_0


==day_0==
~tutorialpt4="complete"
//->tutorial_vera
~music = "upbeat lofi"
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
~music = ""
~task = "Yulia,1,inf,colour:red~yulia_main_1"
~currentSpeaker = "you"
Ok, thanks for putting in that order
And what would you like miss?
~task = "Mei,1,3,colour:pink,height:tall~mei_main_1"
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

