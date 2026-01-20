-- jurassic park sample data population script (postgresql)

SET search_path TO jurassic_park;

-- ============================================
-- islands
-- ============================================
INSERT INTO islands (island_code, island_name, location, established, decommissioned, is_active, description)
VALUES
  ('NUBLAR', 'Isla Nublar', '120 miles west of Costa Rica', '1988-01-01', NULL, true, 'Original Jurassic Park location, later Jurassic World'),
  ('SORNA', 'Isla Sorna', '87 miles southwest of Costa Rica', '1987-01-01', '2004-12-31', false, 'Site B - Production facility for dinosaur cloning'),
  ('MATANCEROS', 'Isla Matanceros', 'Las Cinco Muertes archipelago', '1986-01-01', '1993-12-31', false, 'Early research facility'),
  ('MUERTA', 'Isla Muerta', 'Las Cinco Muertes archipelago', '1986-01-01', '1993-12-31', false, 'Storage and quarantine facility'),
  ('TACANO', 'Isla Tacaño', 'Las Cinco Muertes archipelago', '1987-01-01', '1993-12-31', false, 'Herbivore breeding ground');

-- ============================================
-- employee_roles
-- ============================================
INSERT INTO employee_roles (role_code, role_name, department_name)
VALUES
  ('SCI', 'Scientist', 'Research & Development'),
  ('VET', 'Veterinarian', 'Animal Care'),
  ('SEC', 'Security Officer', 'Security'),
  ('GUIDE', 'Tour Guide', 'Guest Services'),
  ('ENG', 'Engineer', 'Operations'),
  ('TECH', 'Technician', 'Operations'),
  ('MGR', 'Manager', 'Administration'),
  ('HANDLER', 'Animal Handler', 'Animal Care'),
  ('PILOT', 'Pilot', 'Transportation'),
  ('WARDEN', 'Game Warden', 'Security');

-- ============================================
-- employees
-- ============================================
INSERT INTO employees (first_name, last_name, role_id, island_id, hire_date, termination_date, status, email, clearance_level, supervisor_id)
VALUES
  ('John', 'Hammond', (SELECT role_id FROM employee_roles WHERE role_code = 'MGR'), 1, '1985-01-01', '2003-08-01', 'Deceased', 'jhammond@ingen.com', 5, NULL),
  ('Henry', 'Wu', (SELECT role_id FROM employee_roles WHERE role_code = 'SCI'), 1, '1986-06-01', NULL, 'Active', 'hwu@ingen.com', 5, 1),
  ('Robert', 'Muldoon', (SELECT role_id FROM employee_roles WHERE role_code = 'WARDEN'), 1, '1990-03-15', '1993-06-11', 'Deceased', 'rmuldoon@jurassicpark.com', 4, 1),
  ('Ray', 'Arnold', (SELECT role_id FROM employee_roles WHERE role_code = 'ENG'), 1, '1991-02-01', '1993-06-11', 'Deceased', 'rarnold@jurassicpark.com', 4, 1),
  ('Ellie', 'Sattler', (SELECT role_id FROM employee_roles WHERE role_code = 'SCI'), 1, '1993-06-01', '1993-06-15', 'Terminated', 'esattler@paleobotany.edu', 3, NULL),
  ('Alan', 'Grant', (SELECT role_id FROM employee_roles WHERE role_code = 'SCI'), 1, '1993-06-01', '1993-06-15', 'Terminated', 'agrant@paleontology.edu', 3, NULL),
  ('Owen', 'Grady', (SELECT role_id FROM employee_roles WHERE role_code = 'HANDLER'), 1, '2012-05-01', NULL, 'Active', 'ogrady@jurassicworld.com', 4, NULL),
  ('Claire', 'Dearing', (SELECT role_id FROM employee_roles WHERE role_code = 'MGR'), 1, '2011-01-15', NULL, 'Active', 'cdearing@jurassicworld.com', 5, 1),
  ('Barry', 'Sembène', (SELECT role_id FROM employee_roles WHERE role_code = 'HANDLER'), 1, '2012-06-01', NULL, 'Active', 'bsembene@jurassicworld.com', 3, 7),
  ('Vic', 'Hoskins', (SELECT role_id FROM employee_roles WHERE role_code = 'SEC'), 1, '2010-01-01', '2015-06-12', 'Deceased', 'vhoskins@masrani.com', 5, NULL),
  ('Lowery', 'Cruthers', (SELECT role_id FROM employee_roles WHERE role_code = 'TECH'), 1, '2014-03-01', NULL, 'Active', 'lcruthers@jurassicworld.com', 2, NULL),
  ('Vivian', 'Krill', (SELECT role_id FROM employee_roles WHERE role_code = 'TECH'), 1, '2013-07-01', NULL, 'Active', 'vkrill@jurassicworld.com', 2, NULL);

-- ============================================
-- facilities
-- ============================================
INSERT INTO facilities (island_id, facility_name, facility_type, capacity, is_operational, constructed_date, description)
VALUES
  (1, 'Main Visitor Center', 'visitor_center', 500, true, '1992-01-01', 'Central hub for guest services and exhibitions'),
  (1, 'Hammond Creation Lab', 'laboratory', 50, true, '1988-06-01', 'Genetics and cloning laboratory'),
  (1, 'T-Rex Kingdom Paddock', 'paddock', 2, true, '1991-08-01', 'Tyrannosaurus Rex enclosure with 10,000-volt electric fences'),
  (1, 'Raptor Research Arena', 'paddock', 6, true, '2012-01-01', 'Velociraptor training and containment facility'),
  (1, 'Gyrosphere Valley', 'attraction', 50, true, '2014-01-01', 'Self-guided tour through herbivore habitats'),
  (1, 'Mosasaurus Lagoon', 'attraction', 20000, true, '2014-06-01', 'Massive aquatic exhibit for Mosasaurus'),
  (1, 'Indominus Rex Paddock', 'paddock', 1, false, '2014-09-01', 'Maximum security paddock for hybrid species'),
  (2, 'Site B Worker Village', 'facility', 100, false, '1987-01-01', 'Staff housing and operations center'),
  (2, 'Pteranodon Aviary', 'paddock', 50, false, '1989-01-01', 'Large flight dome for flying reptiles');

-- ============================================
-- dinosaur_species
-- ============================================
INSERT INTO dinosaur_species (species_code, common_name, scientific_name, period, diet_type, max_height_meters, max_length_meters, max_weight_kg, danger_level, intelligence_level, social_behavior, description)
VALUES
  ('TREX', 'Tyrannosaurus Rex', 'Tyrannosaurus rex', 'Cretaceous', 'Carnivore', 6.00, 12.30, 8800.00, 5, 3, 'Solitary', 'Apex predator of the Cretaceous period'),
  ('VRAP', 'Velociraptor', 'Velociraptor mongoliensis', 'Cretaceous', 'Carnivore', 1.80, 2.00, 15.00, 4, 5, 'Pack', 'Highly intelligent pack hunter'),
  ('BRACH', 'Brachiosaurus', 'Brachiosaurus altithorax', 'Jurassic', 'Herbivore', 16.00, 26.00, 56000.00, 1, 2, 'Herd', 'Gentle giant herbivore'),
  ('TRIC', 'Triceratops', 'Triceratops horridus', 'Cretaceous', 'Herbivore', 3.00, 9.00, 6000.00, 2, 2, 'Herd', 'Three-horned herbivore'),
  ('DILO', 'Dilophosaurus', 'Dilophosaurus wetherilli', 'Jurassic', 'Carnivore', 2.00, 6.00, 400.00, 3, 3, 'Solitary', 'Venomous carnivore with neck frill'),
  ('GALLI', 'Gallimimus', 'Gallimimus bullatus', 'Cretaceous', 'Omnivore', 2.00, 6.00, 440.00, 1, 3, 'Herd', 'Fast-running ostrich dinosaur'),
  ('MOSA', 'Mosasaurus', 'Mosasaurus hoffmannii', 'Cretaceous', 'Carnivore', 5.00, 18.00, 15000.00, 5, 2, 'Solitary', 'Giant marine reptile'),
  ('PTERA', 'Pteranodon', 'Pteranodon longiceps', 'Cretaceous', 'Carnivore', 1.80, 6.00, 25.00, 2, 3, 'Herd', 'Large flying reptile'),
  ('IREX', 'Indominus Rex', 'Indominus rex', 'Hybrid', 'Carnivore', 6.00, 15.20, 9000.00, 5, 5, 'Solitary', 'Genetically engineered hybrid - extremely dangerous'),
  ('IRAPTOR', 'Indoraptor', 'Indoraptor', 'Hybrid', 'Carnivore', 3.00, 7.30, 1100.00, 5, 5, 'Solitary', 'Weaponized hybrid combining raptor intelligence with enhanced aggression');

-- ============================================
-- dinosaurs
-- ============================================
INSERT INTO dinosaurs (species_id, tag_number, nick_name, sex, birth_date, death_date, status, current_paddock_id, version, health_status, last_checkup_date)
VALUES
  ((SELECT species_id FROM dinosaur_species WHERE species_code = 'TREX'), 'TREX-001', 'Rexy', 'F', '1988-09-15', NULL, 'Alive', 3, '1.0', 'Healthy', '2025-01-15'),
  ((SELECT species_id FROM dinosaur_species WHERE species_code = 'VRAP'), 'VRAP-004', 'Blue', 'F', '2012-05-10', NULL, 'Alive', 4, '2.0', 'Healthy', '2025-01-18'),
  ((SELECT species_id FROM dinosaur_species WHERE species_code = 'VRAP'), 'VRAP-005', 'Charlie', 'F', '2012-05-10', '2015-06-12', 'Deceased', NULL, '2.0', NULL, NULL),
  ((SELECT species_id FROM dinosaur_species WHERE species_code = 'VRAP'), 'VRAP-006', 'Delta', 'F', '2012-05-10', '2015-06-12', 'Deceased', NULL, '2.0', NULL, NULL),
  ((SELECT species_id FROM dinosaur_species WHERE species_code = 'VRAP'), 'VRAP-007', 'Echo', 'F', '2012-05-10', '2015-06-12', 'Deceased', NULL, '2.0', NULL, NULL),
  ((SELECT species_id FROM dinosaur_species WHERE species_code = 'BRACH'), 'BRACH-012', 'Pearl', 'F', '1989-03-22', NULL, 'Alive', 5, '1.0', 'Healthy', '2025-01-10'),
  ((SELECT species_id FROM dinosaur_species WHERE species_code = 'MOSA'), 'MOSA-001', NULL, 'F', '2007-11-05', NULL, 'Alive', 6, '1.0', 'Healthy', '2025-01-12'),
  ((SELECT species_id FROM dinosaur_species WHERE species_code = 'IREX'), 'IREX-001', NULL, 'F', '2012-01-20', '2015-06-12', 'Deceased', NULL, '2.5', NULL, NULL),
  ((SELECT species_id FROM dinosaur_species WHERE species_code = 'TRIC'), 'TRIC-018', 'Trixie', 'F', '1993-02-14', '1993-06-11', 'Deceased', NULL, '1.0', NULL, NULL),
  ((SELECT species_id FROM dinosaur_species WHERE species_code = 'DILO'), 'DILO-003', NULL, 'M', '1990-07-03', '1993-06-11', 'Deceased', NULL, '1.0', NULL, NULL);

-- ============================================
-- incidents
-- ============================================
INSERT INTO incidents (incident_date, incident_type, severity_level, island_id, dinosaur_id, description, casualties, injuries, reported_by_employee_id, status, resolution_date)
VALUES
  ('1993-06-11 14:00:00', 'system_failure', 5, 1, NULL, 'Dennis Nedry disabled security systems to steal embryos, causing park-wide containment failure', 5, 3, 4, 'resolved', '1993-06-11'),
  ('1993-06-11 16:30:00', 'escape', 5, 1, 1, 'T-Rex escaped paddock during storm, attacked tour vehicles', 1, 2, 3, 'resolved', '1993-06-11'),
  ('1993-06-11 21:15:00', 'attack', 5, 1, 10, 'Dilophosaurus attacked Dennis Nedry in his vehicle', 1, 0, NULL, 'resolved', '1993-06-12'),
  ('1993-06-11 22:00:00', 'attack', 5, 1, NULL, 'Velociraptor attack in visitor center kitchen and control room', 2, 1, 6, 'resolved', '1993-06-12'),
  ('2015-06-12 10:00:00', 'escape', 5, 1, 8, 'Indominus Rex breached containment using thermal camouflage and high intelligence', 10, 25, 8, 'resolved', '2015-06-12'),
  ('2015-06-12 14:30:00', 'attack', 4, 1, 2, 'Raptor squad temporarily turned on handlers during I. Rex pursuit', 0, 3, 7, 'resolved', '2015-06-12'),
  ('2015-06-12 15:00:00', 'escape', 4, 1, NULL, 'Pteranodon aviary breach led to aerial attacks on guests', 3, 20, 11, 'resolved', '2015-06-12'),
  ('2018-06-23 09:00:00', 'evacuation', 5, 1, NULL, 'Mount Sibo volcanic eruption forced complete island evacuation', 0, 15, 7, 'resolved', '2018-06-23');

-- ============================================
-- visitors
-- ============================================
INSERT INTO visitors (first_name, last_name, email, phone, country, check_in_date, check_out_date, status, tour_package)
VALUES
  ('Lex', 'Murphy', 'lex.murphy@email.com', '555-0101', 'USA', '1993-06-10', '1993-06-12', 'evacuated', 'VIP'),
  ('Tim', 'Murphy', 'tim.murphy@email.com', '555-0102', 'USA', '1993-06-10', '1993-06-12', 'evacuated', 'VIP'),
  ('Donald', 'Gennaro', 'dgennaro@cowan.com', '555-0103', 'USA', '1993-06-10', '1993-06-12', 'deceased', 'VIP'),
  ('Zach', 'Mitchell', 'zmitchell@email.com', '555-0201', 'USA', '2015-06-10', '2015-06-13', 'evacuated', 'Premium'),
  ('Gray', 'Mitchell', 'gmitchell@email.com', '555-0202', 'USA', '2015-06-10', '2015-06-13', 'evacuated', 'Premium');

-- ============================================
-- vehicles
-- ============================================
INSERT INTO vehicles (vehicle_type, model_name, capacity, island_id, status, last_maintenance_date, assigned_facility_id)
VALUES
  ('tour_vehicle', 'Explorer XJ-450', 5, 1, 'destroyed', '1993-06-01', 1),
  ('tour_vehicle', 'Explorer XJ-450', 5, 1, 'destroyed', '1993-06-01', 1),
  ('gyrosphere', 'Gyrosphere Model 2', 2, 1, 'operational', '2025-01-15', 5),
  ('helicopter', 'Bell 407', 6, 1, 'operational', '2025-01-10', 1),
  ('utility', 'Mercedes G-Wagon', 4, 1, 'operational', '2025-01-12', 1);

-- ============================================
-- tours
-- ============================================
INSERT INTO tours (tour_date, tour_type, guide_employee_id, vehicle_id, capacity, island_id, status)
VALUES
  ('1993-06-11 13:00:00', 'safari_ride', 3, 1, 5, 1, 'emergency_stop'),
  ('2015-06-12 11:00:00', 'gyrosphere', 7, 3, 2, 1, 'emergency_stop'),
  ('2025-01-20 10:00:00', 'safari_ride', 7, 5, 4, 1, 'scheduled');

-- ============================================
-- tour_participants
-- ============================================
INSERT INTO tour_participants (tour_id, visitor_id, waiver_signed, seat_number)
VALUES
  (1, 1, true, 1),
  (1, 2, true, 2),
  (1, 3, true, 3),
  (2, 4, true, 1),
  (2, 5, true, 2);

-- ============================================
-- feeding_schedules
-- ============================================
INSERT INTO feeding_schedules (dinosaur_id, feeding_time, food_type, quantity_kg, assigned_employee_id, status, notes)
VALUES
  (1, '2025-01-20 08:00:00', 'goat', 100.00, 7, 'pending', 't-rex morning feeding'),
  (2, '2025-01-20 09:00:00', 'beef', 15.00, 9, 'pending', 'blue morning feeding'),
  (6, '2025-01-20 07:00:00', 'vegetation', 500.00, NULL, 'pending', 'brachiosaurus morning feeding'),
  (7, '2025-01-20 14:00:00', 'great white shark', 300.00, NULL, 'pending', 'mosasaurus afternoon show feeding');

-- ============================================
-- medical_records
-- ============================================
INSERT INTO medical_records (dinosaur_id, checkup_date, veterinarian_employee_id, weight_kg, temperature_c, diagnosis_notes, treatment, next_checkup_date)
VALUES
  (1, '2025-01-15', NULL, 8750.00, 36.50, 'Excellent health, minor tooth wear', 'None required', '2025-02-15'),
  (2, '2025-01-18', NULL, 14.80, 37.20, 'Healthy, scar tissue healing well from 2015 injuries', 'Vitamin supplement', '2025-02-18'),
  (6, '2025-01-10', NULL, 54000.00, 35.80, 'Good health, minor arthritis in left forelimb', 'Anti-inflammatory medication', '2025-02-10'),
  (7, '2025-01-12', NULL, 14800.00, 34.50, 'Excellent health, teeth show normal wear', 'None required', '2025-02-12');

-- ============================================
-- genetic_samples
-- ============================================
INSERT INTO genetic_samples (species_id, source_type, extraction_date, found_location, viability_percent, storage_facility_id, notes)
VALUES
  ((SELECT species_id FROM dinosaur_species WHERE species_code = 'TREX'), 'amber', '1987-05-15', 'Montana, USA', 87.50, 2, 'Extracted from prehistoric mosquito preserved in amber'),
  ((SELECT species_id FROM dinosaur_species WHERE species_code = 'VRAP'), 'amber', '1987-08-20', 'Mongolia', 92.30, 2, 'High viability sample with excellent DNA integrity'),
  ((SELECT species_id FROM dinosaur_species WHERE species_code = 'BRACH'), 'fossilized bone', '1986-11-10', 'Colorado, USA', 65.00, 2, 'Moderate viability, required frog DNA gap filling'),
  ((SELECT species_id FROM dinosaur_species WHERE species_code = 'MOSA'), 'amber', '2005-03-15', 'Netherlands', 78.50, 2, 'Marine reptile DNA from coastal amber deposit');
