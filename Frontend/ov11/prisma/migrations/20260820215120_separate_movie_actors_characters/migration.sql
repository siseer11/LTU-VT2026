/*
  Warnings:

  - You are about to drop the column `character` on the `Actor` table. All the data in the column will be lost.
  - You are about to drop the `_ActorToMovie` table. If the table is not empty, all the data it contains will be lost.

*/
-- DropForeignKey
ALTER TABLE "_ActorToMovie" DROP CONSTRAINT "_ActorToMovie_A_fkey";

-- DropForeignKey
ALTER TABLE "_ActorToMovie" DROP CONSTRAINT "_ActorToMovie_B_fkey";

-- AlterTable
ALTER TABLE "Actor" DROP COLUMN "character";

-- DropTable
DROP TABLE "_ActorToMovie";

-- CreateTable
CREATE TABLE "ActorCharacter" (
    "id" SERIAL NOT NULL,
    "name" TEXT NOT NULL,
    "image" TEXT,
    "role" TEXT,
    "actorId" INTEGER NOT NULL,
    "movieId" INTEGER NOT NULL,

    CONSTRAINT "ActorCharacter_pkey" PRIMARY KEY ("id")
);

-- CreateIndex
CREATE UNIQUE INDEX "ActorCharacter_actorId_movieId_key" ON "ActorCharacter"("actorId", "movieId");

-- AddForeignKey
ALTER TABLE "ActorCharacter" ADD CONSTRAINT "ActorCharacter_actorId_fkey" FOREIGN KEY ("actorId") REFERENCES "Actor"("id") ON DELETE RESTRICT ON UPDATE CASCADE;

-- AddForeignKey
ALTER TABLE "ActorCharacter" ADD CONSTRAINT "ActorCharacter_movieId_fkey" FOREIGN KEY ("movieId") REFERENCES "Movie"("id") ON DELETE RESTRICT ON UPDATE CASCADE;
