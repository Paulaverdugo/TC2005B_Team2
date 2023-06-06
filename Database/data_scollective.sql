USE scollective;

-- USER_INFO (user_name, user_password, email, age, user_register)
INSERT INTO USER_INFO VALUES
("andres_tarazona", "andres123", "andres@gmail.com", 23),
("dulce_garcia", "dulce123", "dulce@gmail.com", 20),
("mariel_gomez", "mariel123", "mariel@gmail.com", 19),
("paula_verdugo", "paula123", "paula@gmail.com", 20),
("santiago_rodriguez", "santiago123", "santiago@gmail.com", 21);

-- PLAYER_TYPES (ID, name_ptypes, life_points, speed)
INSERT INTO PLAYER_TYPES (name_ptypes, life_points, speed) VALUES
("Cybergladiator", 1, 10),
("Codebreaker", 2, 20),
("Ghostwalker", 3, 3);

-- GADGET (ID, gadget_name, gadget_description, player_type)
INSERT INTO GADGET (gadget_name, gadget_description, player_type) VALUES
("Cyber Dash", "A dash ability", 1),
("Bio Stim", "A one time use healing ability", 1),
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
("2023-04-30", "andres_tarazona", 3),
("2023-04-27", "dulce_garcia", 2), 
("2023-04-27", "dulce_garcia", 3), 
("2023-04-29", "mariel_gomez", 3), 
("2023-04-29", "mariel_gomez", 1), 
("2023-04-20", "paula_verdugo", 1), 
("2023-04-20", "paula_verdugo", 2), 
("2023-04-21", "santiago_rodriguez", 3),
("2023-04-21", "santiago_rodriguez", 2);

-- PROGRESS (ID, level_achieved, user_name, player_type)
INSERT INTO PROGRESS (level_achieved, user_name, player_type) VALUES
(3, "andres_tarazona", 1),
(3, "andres_tarazona", 1),
(3, "andres_tarazona", 1),
(2, "dulce_garcia", 3),
(2, "dulce_garcia", 2),
(2,  "mariel_gomez", 3),
(1, "paula_verdugo", 1),
(2, "paula_verdugo", 2),
(2, "santiago_rodriguez", 3),
(1, "santiago_rodriguez", 2);

-- CHOSEN_GADGET (ID, progress_id, gadget_id)
INSERT INTO CHOSEN_GADGET (progress_id, gadget_id) VALUES
(1, 1),
(2, 1),
(3, 3),
(4, 6),
(5, 7),
(6, 5),
(7, 3),
(8, 1),
(9, 2),
(10, 8);

-- WINS (ID, user_name, player_type)
INSERT INTO WINS (user_name, player_type) VALUES
("andres_tarazona", 1),
("andres_tarazona", 1),
("andres_tarazona", 3),
("dulce_garcia", 3),
("dulce_garcia", 2),
("mariel_gomez", 3),
("mariel_gomez", 1),
("paula_verdugo", 1),
("santiago_rodriguez", 2);

-- DEADS (ID, user_name, player_type, level_dead)
INSERT INTO DEADS (user_name, player_type, level_dead) VALUES
("andres_tarazona", 1, 2),
("andres_tarazona", 1, 3),
("andres_tarazona", 3, 1),
("dulce_garcia", 3, 2),
("dulce_garcia", 2, 3),
("mariel_gomez", 3, 1),
("mariel_gomez", 1, 2),
("paula_verdugo", 1, 3),
("santiago_rodriguez", 2, 1);










