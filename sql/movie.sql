
DO $$ 
    BEGIN


    -- create movie table
    CREATE TABLE IF NOT EXISTS classifierprototypeservice.movie (
        id serial NOT NULL,
        title text  DEFAULT NULL,
        genre text  DEFAULT NULL,
        releasedate timestamp without time zone DEFAULT NULL,
        CONSTRAINT movie_pkey PRIMARY KEY (id)
    )
    WITH (
        OIDS=FALSE
    );


    -- insert some test values
    IF (NOT EXISTS (select 1 from classifierprototypeservice.movie where id = 1)) THEN
        INSERT INTO classifierprototypeservice.movie (id, title, genre, releasedate)
        VALUES(1, 'test title 1', 'test genre 1', now());
    END IF;
    
    IF (NOT EXISTS (select 2 from classifierprototypeservice.movie where id = 2)) THEN
        INSERT INTO classifierprototypeservice.movie (id, title, genre, releasedate)
        VALUES(2, 'test title 2', 'test genre 2', now());
    END IF;
    
    IF (NOT EXISTS (select 3 from classifierprototypeservice.movie where id = 3)) THEN
        INSERT INTO classifierprototypeservice.movie (id, title, genre, releasedate)
        VALUES(3, 'test title 3', 'test genre 3', now());
    END IF;

    END;
$$