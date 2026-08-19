/*
  Warnings:

  - You are about to alter the column `voteAverage` on the `Movie` table. The data in that column could be lost. The data in that column will be cast from `Decimal(65,30)` to `Integer`.
  - You are about to alter the column `rating` on the `Review` table. The data in that column could be lost. The data in that column will be cast from `Decimal(65,30)` to `Integer`.

*/
-- AlterTable
ALTER TABLE "Movie" ALTER COLUMN "voteAverage" SET DATA TYPE INTEGER;

-- AlterTable
ALTER TABLE "Review" ALTER COLUMN "rating" SET DATA TYPE INTEGER;
