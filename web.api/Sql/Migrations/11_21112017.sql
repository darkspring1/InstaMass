START TRANSACTION;
SELECT migrations.set_migration(11);

ALTER TABLE "TagTasks" ADD COLUMN "LastPost" varchar(100) NOT NULL;
ALTER TABLE "TagTasks" ADD COLUMN "Posts" varchar(100) NOT NULL;
ALTER TABLE "TagTasks" ADD COLUMN "Followers" varchar(100) NOT NULL;
ALTER TABLE "TagTasks" ADD COLUMN "Followings" varchar(100) NOT NULL;
ALTER TABLE "TagTasks" ADD COLUMN "AvatarExistDisabled" boolean NOT NULL;

COMMIT TRANSACTION;