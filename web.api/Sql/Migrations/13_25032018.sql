START TRANSACTION;
SELECT migrations.set_migration(13);

CREATE TABLE public."EntityStatuses"(
	"Id" int NOT NULL,
	"Status" text NOT NULL,
	CONSTRAINT "ObjectStatuses_pkey" PRIMARY KEY ("Id")
);

insert into public."EntityStatuses" ("Id", "Status") values(1, 'active');
insert into public."EntityStatuses" ("Id", "Status") values(2, 'deleted');

ALTER TABLE public."Tasks" ADD COLUMN "EntityStatusId" int not null default 1;
ALTER TABLE public."Tasks" ALTER COLUMN "EntityStatusId" DROP DEFAULT;

COMMIT TRANSACTION;