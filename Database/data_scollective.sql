USE scollective;

-- USER_INFO (user_name, user_password, email, age, user_register)
INSERT INTO USER_INFO VALUES
("andres_tarazona", "andres123", "andres@gmail.com", 23, "2023-05-25"),
("dulce_garcia", "dulce123", "dulce@gmail.com", 20, "2023-05-25"),
("mariel_gomez", "mariel123", "mariel@gmail.com", 19, "2023-05-25"),
("paula_verdugo", "paula123", "paula@gmail.com", 20, "2023-05-25"),
("santiago_rodriguez", "santiago123", "santiago@gmail.com", 21, "2023-05-25");

-- PLAYER_TYPES (ID, name_ptypes, life_points, speed)
INSERT INTO PLAYER_TYPES (name_ptypes, life_points, speed) VALUES
("Cybergladiator", 1, 10),
("Codebreaker", 2, 20),
("Ghostwalker", 3, 3);

-- LIFE (ID, life_points, life_stamp)
INSERT INTO LIFE (life_points, life_stamp) VALUES
(1, "2023-05-30 09:30:01"),
(5, "2023-05-30 09:30:01"),
(7, "2023-05-30 09:30:01"),
(2, "2023-05-30 09:30:01"),
(3, "2023-05-30 09:30:01");

-- GADGET (ID, gadget_name, gadget_description, player_type)
INSERT INTO GADGET (gadget_name, gadget_description, player_type) VALUES
("Cyber Rush", "A dash ability", 1),
("Plasma Shield", "A shield that blocks all damage briefly", 1),
("Overcharge", "Briefly doubles all damage", 1),
("Shadow Veil", "A cloak that when seen hacks the enemy that saw you", 2),
("Circuit Breaker", "An EMP surge that hacks enemies inside a range for 5 seconds", 2),
("Phantom Signal", "A fake decoy that sends an alarm with the wrong coordinates", 2),
("Ghost Vision", "The player can see vision cones", 3),
("Phantom Step", "Dash when stealthing", 3),
("Ghost Blade", "One-hit takedown while stealthing. One use only", 3);

-- PLAYER (ID, last_connection, user_name, player_type)
INSERT INTO PLAYER (last_connection, user_name, player_type) VALUES 
("2023-04-30", "andres_tarazona", 1), 
("2023-04-27", "dulce_garcia", 2), 
("2023-04-29", "mariel_gomez", 3), 
("2023-04-20", "paula_verdugo", 1), 
("2023-04-21", "santiago_rodriguez", 2);

-- PROGRESS (ID, level_achieved, user_name, player_type, life_points)
INSERT INTO PROGRESS (level_achieved, user_name, player_type, life_points) VALUES
(1, "andres_tarazona", 1, 1),
(2, "dulce_garcia", 3, 2),
(2,  "mariel_gomez", 3, 3),
(1, "paula_verdugo", 1, 4),
(1, "santiago_rodriguez", 2, 5);

-- WINS (ID, user_name, player_type)
INSERT INTO WINS (user_name, player_type) VALUES
("andres_tarazona", 1),
("andres_tarazona", 1),
("andres_tarazona", 3),
("dulce_garcia", 3),
("dulce_garcia", 2),
("mariel_gomez", 3),
"mariel_gomez", 1),
("paula_verdugo", 1),
("santiago_rodriguez", 2);










