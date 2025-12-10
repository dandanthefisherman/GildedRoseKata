# How I have refactored the gilded rose kata

I firstly created feature branch as my base branch.

All pull requests were pushed into that branch.

All commits into every branch were gradual and safe. At no point did I commit code that would not compile. You should be able to revert the last commit and the code will still compile.

The first thing I did was to write some unit tests to verify that the existing legacy code works as expected. 

I then wrote some tests for the conjured items, knowing they would fail. I then updated the existing legacy code so that the updater would correctly update conjured items, after which the unit tests I wrote passed.

At this point i was not happy with the way the unit tests were displaying in the test runner, so I refactored then to include a test description to be displayed in the test runner. I also added some more unit tests to verify that the item updater can handle multiple different item types all at once. I also renamed the approval test.

Next, I added a domain project and an application project. I wanted the domain entities and contracts to live in this project and a services that update items to live in the application project. This pattern a clean architecture/ddd pattern. As you can see, Nothing is referenced by the domain project. the application project references the domain project and the console app references both the application and domain, sort of mini onion architecture.

Next, I transferred the gilded rose logic into those two projects, creating a service to be accessed by the console app.

I then updated the tests and console app to use the new service. I then deleted the old legacy code.

The unit tests confirmed that the refactor was successful.

I then create an application test project and moved the unit tests to this new project. The test file was quite large, so I split the unit tests up into smaller test files, based on the item type.

I then added an interface to the item updater service and made sure the service was only accessible though the interface. I updated the tests and console app to use the interface. The unit tests verified that the refactor worked.

Lastly, I tidied up the console app, pulling out some of the clutter in the program file.

Then after sleeping on it, I decided to move the service collections extension file into the console app, in line with clean architecture practices.

# What else would I have liked to do?

You can see in the application folder the Itemupdatefactory class. It contains magic strings which im not comfortable with! Ideally, I would have liked to change the item.cs class to include an enum of ItemType, which would have had all the different possible item types. It states in the read me that this should not be changed. I can talk you through what else I considered to get rid of the magic strings should we have chance to speak again! I was unsure how far to go with this exercise and could have kept refactoring forever, so i made the decision to leave it as it is now.

Cheers, Dan



# Gilded Rose Refactoring Kata

## How to use this Kata

The first thing to do is make a copy of this repository in your personal GitHub account and make public. Please do not fork this repository as your solution will be visible to others undertaking this exercise.

The purpose of this exercise is to demonstrate your skills at designing test cases and refactoring a legacy codebase, safely. 

The idea is not to re-write the code from scratch, but rather to practice taking small steps, running the tests often, and incrementally improve the design towards a solution that embodies OO principles and patterns.


## Gilded Rose Requirements Specification

Hi and welcome to team Gilded Rose. As you know, we are a small inn with a prime location in a
prominent city ran by a friendly innkeeper named Allison. We also buy and sell only the finest goods.
Unfortunately, our goods are constantly degrading in `Quality` as they approach their sell by date.

We have a system in place that updates our inventory for us. It was developed by a no-nonsense type named
Leeroy, who has moved on to new adventures. Your task is to add the new feature to our system so that
we can begin selling a new category of items. First an introduction to our system:

- All `items` have a `SellIn` value which denotes the number of days we have to sell the `items`
- All `items` have a `Quality` value which denotes how valuable the item is
- At the end of each day our system lowers both values for every item

Pretty simple, right? Well this is where it gets interesting:

- Once the sell by date has passed, `Quality` degrades twice as fast
- The `Quality` of an item is never negative
- __"Aged Brie"__ actually increases in `Quality` the older it gets
- The `Quality` of an item is never more than `50`
- __"Sulfuras"__, being a legendary item, never has to be sold or decreases in `Quality`
- __"Backstage passes"__, like aged brie, increases in `Quality` as its `SellIn` value approaches;
	- `Quality` increases by `2` when there are `10` days or less and by `3` when there are `5` days or less but
	- `Quality` drops to `0` after the concert

We have recently signed a supplier of conjured items. This requires an update to our system:

- __"Conjured"__ items degrade in `Quality` twice as fast as normal items

Feel free to make any changes to the `UpdateQuality` method and add any new code as long as everything
still works correctly. However, do not alter the `Item` class or `Items` property as those belong to the
goblin in the corner who will insta-rage and one-shot you as he doesn't believe in shared code
ownership (you can make the `UpdateQuality` method and `Items` property static if you like, we'll cover
for you).

Just for clarification, an item can never have its `Quality` increase above `50`, however __"Sulfuras"__ is a
legendary item and as such its `Quality` is `80` and it never alters.


## Introduction to Text-Based Approval Testing
This is a testing approach which is very useful when refactoring legacy code. Before you change the code, you run it, and gather the output of the code as a plain text file. You review the text, and if it correctly describes the behaviour as you understand it, you can "approve" it, and save it as a "Golden Master". Then after you change the code, you run it again, and compare the new output against the Golden Master. Any differences, and the test fails.

It's basically the same idea as "assertEquals(expected, actual)" in a unit test, except the text you are comparing is typically much longer, and the "expected" value is saved from actual output, rather than being defined in advance.

Typically a piece of legacy code may not produce suitable textual output from the start, so you may need to modify it before you can write your first text-based approval test. This has already been setup and the initial "Golden Master" has been provided in `ApprovalTest.ThirtyDays.verified.txt`