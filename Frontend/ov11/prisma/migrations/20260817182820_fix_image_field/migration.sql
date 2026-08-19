/*
  Warnings:

  - You are about to drop the column `iage` on the `Actor` table. All the data in the column will be lost.

*/
-- AlterTable
ALTER TABLE "Actor" DROP COLUMN "iage",
ADD COLUMN     "image" TEXT;
