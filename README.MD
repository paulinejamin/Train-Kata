# Train Kata

This kata is a simple one used to introduce DDD concepts and patterns. The tests have already been written to focus the effort on the refactoring phase.

*__Note:__ this kata has been directly inspired by Emily BACHE's __[KataTrainReservation](https://github.com/emilybache/KataTrainReservation)__ .


### Existing business rules

1. You cannot book seats if there is not enough seats available in the train.
2. You must put all the seats for one reservation in the same coach.

## Objectives

### Introduce new business rules
1. For a train overall, no more than 70% of seats may be reserved in advance.
2. For a coach overall, no more than 80% of seats may be reserved in advance.

### Nice-to-have
3. Define an __Ubiquitous language__
4. Use as much as possible __DDD tactical patterns__
5. Implement the __Hexagonal Architecture__
6. Design better tests