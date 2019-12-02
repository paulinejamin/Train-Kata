# Kata: Train Reservation
Railway operators aren't always known for their use of cutting edge technology, and in this case they're a little behind the times. The railway people want you to help them to improve their online booking service. They'd like to be able to not only sell tickets online, but to decide exactly which seats should be reserved, at the time of booking.

You're working on the "TicketOffice" service, and your next task is to implement the feature for reserving seats on a particular train. The railway operator has a service-oriented architecture, and both the interface you'll need to fulfill, and some services you'll need to use are already implemented.

Business Rules around Reservations
There are various business rules and policies around which seats may be reserved. For a train overall, no more than 80% of seats may be reserved in advance, and ideally no individual coach should have no more than 80% reserved seats either. However, there is another business rule that says you must put all the seats for one reservation in the same coach. This could make you and go over 80% for some coaches, just make sure to keep to 80% for the whole train.

The Guiding Test
The Ticket Office service needs to respond to a HTTP POST request that comes with form data telling you which train the customer wants to reserve seats on, and how many they want. It should return a json document detailing the reservation that has been made.

A reservation comprises a json document with three fields, the train id, booking reference, and the ids of the seats that have been reserved. Example json:

{"train_id": "express_2000", "booking_reference": "75bcd15", "seats": ["1A", "1B"]}
If it is not possible to find suitable seats to reserve, the service should instead return an empty list of seats and an empty string for the booking reference.