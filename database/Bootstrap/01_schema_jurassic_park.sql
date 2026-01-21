-- jurassic park database schema (postgresql)
-- based on the jurassic park and jurassic world franchise

-- drop existing schema if it exists
-- Reduce chatter so the GitHub Action doesn't treat NOTICE as an error
SET client_min_messages = WARNING;

DROP SCHEMA IF EXISTS jurassic_park CASCADE;
CREATE SCHEMA jurassic_park;

SET client_min_messages = NOTICE;

SET search_path TO jurassic_park;

-- ============================================
-- core tables
-- ============================================

-- islands (park locations)
CREATE TABLE IF NOT EXISTS islands (
  island_id          SERIAL PRIMARY KEY,
  island_code        VARCHAR(20) NOT NULL UNIQUE,
  island_name        VARCHAR(100) NOT NULL,
  location           VARCHAR(200),
  established        DATE,
  decommissioned     DATE,
  is_active          BOOLEAN NOT NULL DEFAULT true,
  description        TEXT
);

-- facilities (buildings, paddocks, labs, etc.)
CREATE TABLE IF NOT EXISTS facilities (
  facility_id           SERIAL PRIMARY KEY,
  island_id             INTEGER REFERENCES islands(island_id) ON UPDATE CASCADE ON DELETE SET NULL,
  facility_name         VARCHAR(200) NOT NULL,
  facility_type         VARCHAR(50) NOT NULL, -- visitor_center, lab, paddock, etc.
  capacity              INTEGER,
  is_operational        BOOLEAN NOT NULL DEFAULT true,
  constructed_date      DATE,
  decommissioned_date   DATE,
  description           TEXT
);

-- employee roles
CREATE TABLE IF NOT EXISTS employee_roles (
  role_id            SERIAL PRIMARY KEY,
  role_code          VARCHAR(20) NOT NULL UNIQUE,
  role_name          VARCHAR(100) NOT NULL,
  department_name    VARCHAR(100)
);

-- employees
CREATE TABLE IF NOT EXISTS employees (
  employee_id           SERIAL PRIMARY KEY,
  first_name            VARCHAR(100) NOT NULL,
  last_name             VARCHAR(100) NOT NULL,
  role_id               INTEGER REFERENCES employee_roles(role_id) ON UPDATE CASCADE ON DELETE SET NULL,
  island_id             INTEGER REFERENCES islands(island_id) ON UPDATE CASCADE ON DELETE SET NULL,
  hire_date             DATE,
  termination_date      DATE,
  status                VARCHAR(50) NOT NULL DEFAULT 'Active', -- Active, Deceased, Terminated, Missing
  email                 VARCHAR(200),
  phone                 VARCHAR(50),
  emergency_contact     VARCHAR(200),
  clearance_level       INTEGER CHECK (clearance_level BETWEEN 1 AND 5),
  supervisor_id         INTEGER REFERENCES employees(employee_id) ON UPDATE CASCADE ON DELETE SET NULL
);

-- dinosaur species catalog
CREATE TABLE IF NOT EXISTS dinosaur_species (
  species_id          SERIAL PRIMARY KEY,
  species_code        VARCHAR(20) NOT NULL UNIQUE,
  common_name         VARCHAR(100) NOT NULL,
  scientific_name     VARCHAR(200) NOT NULL,
  period              VARCHAR(50), -- jurassic, cretaceous, etc.
  diet_type           VARCHAR(50), -- carnivore, herbivore, omnivore
  max_height_meters   NUMERIC(5,2),
  max_length_meters   NUMERIC(5,2),
  max_weight_kg       NUMERIC(8,2),
  danger_level        INTEGER CHECK (danger_level BETWEEN 1 AND 5),
  intelligence_level  INTEGER CHECK (intelligence_level BETWEEN 1 AND 5),
  social_behavior     VARCHAR(50), -- solitary, pack, herd
  description         TEXT
);

-- individual dinosaurs
CREATE TABLE IF NOT EXISTS dinosaurs (
  dinosaur_id             SERIAL PRIMARY KEY,
  species_id              INTEGER NOT NULL REFERENCES dinosaur_species(species_id) ON UPDATE CASCADE ON DELETE RESTRICT,
  tag_number              VARCHAR(50) NOT NULL UNIQUE,
  nick_name               VARCHAR(100),
  sex                     CHAR(1) CHECK (sex IN ('M', 'F', 'U')), -- male, female, unknown
  birth_date              DATE,
  death_date              DATE,
  status                  VARCHAR(50) NOT NULL DEFAULT 'Alive', -- alive, deceased, escaped, quarantined
  current_paddock_id      INTEGER REFERENCES facilities(facility_id) ON UPDATE CASCADE ON DELETE SET NULL,
  version                 VARCHAR(20), -- genetic iteration (e.g., "2.3")
  genetic_modifications   TEXT,
  health_status           VARCHAR(100),
  last_checkup_date       DATE
);

-- genetic samples (dna sources)
CREATE TABLE IF NOT EXISTS genetic_samples (
  sample_id             SERIAL PRIMARY KEY,
  species_id            INTEGER REFERENCES dinosaur_species(species_id) ON UPDATE CASCADE ON DELETE SET NULL,
  source_type           VARCHAR(100), -- amber, fossilized bone, etc.
  extraction_date       DATE,
  found_location        VARCHAR(200),
  viability_percent     NUMERIC(5,2) CHECK (viability_percent BETWEEN 0 AND 100),
  storage_facility_id   INTEGER REFERENCES facilities(facility_id) ON UPDATE CASCADE ON DELETE SET NULL,
  notes                 TEXT
);

-- incidents (escapes, attacks, system failures)
CREATE TABLE IF NOT EXISTS incidents (
  incident_id               SERIAL PRIMARY KEY,
  incident_date             TIMESTAMP NOT NULL,
  incident_type             VARCHAR(50) NOT NULL, -- escape, attack, system_failure, etc.
  severity_level            INTEGER CHECK (severity_level BETWEEN 1 AND 5),
  island_id                 INTEGER REFERENCES islands(island_id) ON UPDATE CASCADE ON DELETE SET NULL,
  facility_id               INTEGER REFERENCES facilities(facility_id) ON UPDATE CASCADE ON DELETE SET NULL,
  dinosaur_id               INTEGER REFERENCES dinosaurs(dinosaur_id) ON UPDATE CASCADE ON DELETE SET NULL,
  description               TEXT NOT NULL,
  casualties                INTEGER DEFAULT 0,
  injuries                  INTEGER DEFAULT 0,
  reported_by_employee_id   INTEGER REFERENCES employees(employee_id) ON UPDATE CASCADE ON DELETE SET NULL,
  status                    VARCHAR(50) NOT NULL DEFAULT 'Under Investigation', -- resolved, under_investigation, unresolved
  resolution_date           DATE
);

-- visitors
CREATE TABLE IF NOT EXISTS visitors (
  visitor_id          SERIAL PRIMARY KEY,
  first_name          VARCHAR(100) NOT NULL,
  last_name           VARCHAR(100) NOT NULL,
  email               VARCHAR(200),
  phone               VARCHAR(50),
  country             VARCHAR(100),
  emergency_contact   VARCHAR(200),
  check_in_date       DATE NOT NULL,
  check_out_date      DATE,
  status              VARCHAR(50) NOT NULL DEFAULT 'Active', -- active, checked_out, evacuated, deceased, missing
  tour_package        VARCHAR(50) -- basic, premium, vip
);

-- vehicles (tour vehicles, utility vehicles, helicopters)
CREATE TABLE IF NOT EXISTS vehicles (
  vehicle_id               SERIAL PRIMARY KEY,
  vehicle_type             VARCHAR(50) NOT NULL, -- tour_vehicle, gyrosphere, helicopter, etc.
  model_name               VARCHAR(100),
  capacity                 INTEGER,
  island_id                INTEGER REFERENCES islands(island_id) ON UPDATE CASCADE ON DELETE SET NULL,
  status                   VARCHAR(50) NOT NULL DEFAULT 'Operational', -- operational, maintenance, destroyed
  last_maintenance_date    DATE,
  assigned_facility_id     INTEGER REFERENCES facilities(facility_id) ON UPDATE CASCADE ON DELETE SET NULL
);

-- tours
CREATE TABLE IF NOT EXISTS tours (
  tour_id              SERIAL PRIMARY KEY,
  tour_date            TIMESTAMP NOT NULL,
  tour_type            VARCHAR(50) NOT NULL, -- gyrosphere, kayak, safari_ride, etc.
  guide_employee_id    INTEGER REFERENCES employees(employee_id) ON UPDATE CASCADE ON DELETE SET NULL,
  vehicle_id           INTEGER REFERENCES vehicles(vehicle_id) ON UPDATE CASCADE ON DELETE SET NULL,
  capacity             INTEGER NOT NULL,
  island_id            INTEGER REFERENCES islands(island_id) ON UPDATE CASCADE ON DELETE SET NULL,
  status               VARCHAR(50) NOT NULL DEFAULT 'Scheduled' -- scheduled, completed, cancelled, emergency_stop
);

-- tour participants (many-to-many)
CREATE TABLE IF NOT EXISTS tour_participants (
  tour_id              INTEGER NOT NULL REFERENCES tours(tour_id) ON UPDATE CASCADE ON DELETE CASCADE,
  visitor_id           INTEGER NOT NULL REFERENCES visitors(visitor_id) ON UPDATE CASCADE ON DELETE CASCADE,
  waiver_signed        BOOLEAN NOT NULL DEFAULT false,
  seat_number          INTEGER,
  PRIMARY KEY (tour_id, visitor_id)
);

-- feeding schedules
CREATE TABLE IF NOT EXISTS feeding_schedules (
  feeding_id             SERIAL PRIMARY KEY,
  dinosaur_id            INTEGER NOT NULL REFERENCES dinosaurs(dinosaur_id) ON UPDATE CASCADE ON DELETE CASCADE,
  feeding_time           TIMESTAMP NOT NULL,
  food_type              VARCHAR(100), -- goat, fish, vegetation
  quantity_kg            NUMERIC(8,2),
  assigned_employee_id   INTEGER REFERENCES employees(employee_id) ON UPDATE CASCADE ON DELETE SET NULL,
  status                 VARCHAR(50) NOT NULL DEFAULT 'Pending', -- pending, completed, missed
  notes                  TEXT
);

-- medical records (veterinary)
CREATE TABLE IF NOT EXISTS medical_records (
  record_id                   SERIAL PRIMARY KEY,
  dinosaur_id                 INTEGER NOT NULL REFERENCES dinosaurs(dinosaur_id) ON UPDATE CASCADE ON DELETE CASCADE,
  checkup_date                DATE NOT NULL,
  veterinarian_employee_id    INTEGER REFERENCES employees(employee_id) ON UPDATE CASCADE ON DELETE SET NULL,
  weight_kg                   NUMERIC(8,2),
  temperature_c               NUMERIC(5,2),
  diagnosis_notes             TEXT,
  treatment                   TEXT,
  next_checkup_date           DATE
);

-- ============================================
-- indexes
-- ============================================

CREATE INDEX IF NOT EXISTS idx_facilities_island   ON facilities(island_id);
CREATE INDEX IF NOT EXISTS idx_employees_island    ON employees(island_id);
CREATE INDEX IF NOT EXISTS idx_employees_role      ON employees(role_id);
CREATE INDEX IF NOT EXISTS idx_dinosaurs_species   ON dinosaurs(species_id);
CREATE INDEX IF NOT EXISTS idx_dinosaurs_paddock   ON dinosaurs(current_paddock_id);
CREATE INDEX IF NOT EXISTS idx_incidents_island    ON incidents(island_id);
CREATE INDEX IF NOT EXISTS idx_incidents_date      ON incidents(incident_date);
CREATE INDEX IF NOT EXISTS idx_tours_date          ON tours(tour_date);
CREATE INDEX IF NOT EXISTS idx_tours_island        ON tours(island_id);
CREATE INDEX IF NOT EXISTS idx_feeding_dinosaur    ON feeding_schedules(dinosaur_id);
CREATE INDEX IF NOT EXISTS idx_medical_dinosaur    ON medical_records(dinosaur_id);

-- ============================================
-- comments
-- ============================================

COMMENT ON SCHEMA jurassic_park     IS 'Jurassic Park management database - tracks dinosaurs, employees, facilities, incidents, and park operations';
COMMENT ON TABLE islands            IS 'park locations and islands';
COMMENT ON TABLE facilities         IS 'buildings, paddocks, labs, and other park structures';
COMMENT ON TABLE dinosaur_species   IS 'catalog of dinosaur species with characteristics';
COMMENT ON TABLE dinosaurs          IS 'individual dinosaur specimens tracked by the park';
COMMENT ON TABLE incidents          IS 'security breaches, escapes, attacks, and other incidents';
