START TRANSACTION;

SELECT migrations.set_migration(1);

CREATE TABLE public."Users"
(
    "Id" UUID NOT NULL,
    "PasswordHash" character varying(1024) COLLATE pg_catalog."default",
    "Email" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "UserName" character varying(100) COLLATE pg_catalog."default" NOT NULL,
    "CreatedAt" timestamp without time zone NOT NULL,
    CONSTRAINT "Users_pkey" PRIMARY KEY ("Id")
);

CREATE TABLE public."ExternalAuthProviderTypes"
(
    "Id" integer NOT NULL,
    "Type" character varying(50) UNIQUE COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT "ExternalAuthProviderTypes_pkey" PRIMARY KEY ("Id")
);

CREATE TABLE public."ExternalAuthProviders"
(
    "ExternalUserId" character varying(50) COLLATE pg_catalog."default" NOT NULL,
    "ExternalAuthProviderTypeId" integer NOT NULL,
    "UserId" UUID NOT NULL,
    
    CONSTRAINT "ExternalAuthProviders_pkey" PRIMARY KEY ("ExternalUserId", "ExternalAuthProviderTypeId"),
    
    CONSTRAINT "FK_ExternalAuthProviders_ExternalAuthProviderTypes.Id" FOREIGN KEY ("ExternalAuthProviderTypeId")
	      REFERENCES public."ExternalAuthProviderTypes" ("Id") MATCH SIMPLE
	      ON UPDATE NO ACTION ON DELETE CASCADE,
    
    CONSTRAINT "FK_ExternalAuthProviders_Users.Id" FOREIGN KEY ("UserId")
	      REFERENCES public."Users" ("Id") MATCH SIMPLE
	      ON UPDATE NO ACTION ON DELETE CASCADE
);
 
COMMIT TRANSACTION;