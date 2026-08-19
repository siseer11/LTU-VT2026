"use server";

import { z } from "zod";
import prisma from "@/lib/prisma";
import { revalidatePath } from "next/cache";

const movieCreatingSchema = z.object({
	title: z.string().min(2, { message: "Title is required, min 2 chars." }).max(50, { message: "Title is too long, max 50 chars." }),
	tagline: z.string().min(5, { message: "Tagline is required, min 5 chars." }).max(100, { message: "Tagline is too long, max 100 chars." }),
	description: z.string().min(50, { message: "Description is required, min 50 chars." }).max(500, { message: "Description is too long, max 500 chars." }),
	releaseDate: z.string().min(1, { message: "Release date is required." }),
	duration: z.number().min(1, { message: "Duration is required." }),
	poster: z.string().url().startsWith("https://image.tmdb.org/", { message: "Poster must come from the tmdb." }).optional(),
	backdrop: z.string().url().startsWith("https://image.tmdb.org/", { message: "Backdrop must come from the tmdb." }).optional(),
	budget: z.number().min(1, {message: "Budget can not be negative."}).optional(),
	score: z.number().min(1, {message: "Score can not be negative."}).max(100, { message: "Score can not be greater than 100." }).optional(),
	numberOfVotes: z.number().min(0, {message: "Number of votes can not be negative."}).optional(),
	genres: z.array(z.number()).min(1, { message: "At least one genre is required." }).max(4, { message: "At most 4 genres are allowed." }),
	actors: z.array(z.number()).min(1, { message: "At least one actor is required." }),
}).refine(data => {
	return data.score === undefined || data.numberOfVotes !== undefined;
}, { message: "Number of votes is required when score is provided.", path: ["numberOfVotes"] });

interface MovieFormState {
	success?: {
		newMovieId: number;
		status: boolean;
	};
	errors?: Record<string, string[]>;
	values?: Partial<z.infer<typeof movieCreatingSchema>>;
}

const getNumberOrUndefinedOptionalFields = (inputValue: string) => {
	return inputValue === "" ? undefined : Number(inputValue);
}

const getStringOrUndefinedOptionalFields = (inputValue: string) => {
	return inputValue === "" ? undefined : inputValue;
}

export async function createMovie(_prevFormData: MovieFormState, formData: FormData ): Promise<MovieFormState> {
	const raw = {
		title: formData.get("title") as string,
		tagline: formData.get("tagline") as string,
		description: formData.get("description") as string,
		releaseDate: formData.get("releaseDate") as string,
		duration: Number(formData.get("duration")),
		poster: getStringOrUndefinedOptionalFields(formData.get("poster") as string),
		backdrop: getStringOrUndefinedOptionalFields(formData.get("backdrop") as string),
		budget: getNumberOrUndefinedOptionalFields(formData.get("budget") as string),
		score: getNumberOrUndefinedOptionalFields(formData.get("score") as string),
		numberOfVotes: getNumberOrUndefinedOptionalFields(formData.get("numberOfVotes") as string),
		genres: formData.getAll("genres")?.map(el => Number(el)) as number[],
		actors: formData.getAll("actors")?.map(el => Number(el)) as number[],
	}

	const parsed = movieCreatingSchema.safeParse(raw);

	if (!parsed.success) {
		return {
			errors: parsed.error.flatten().fieldErrors,
			values: raw,
		};
	}
		
	const newMovie = await prisma.movie.create({
		data: {
			title: parsed.data.title,
			tagline: parsed.data.tagline,
			description: parsed.data.description,
			releaseDate: new Date(parsed.data.releaseDate),
			duration: parsed.data.duration,
			poster: parsed.data.poster,
			backdrop: parsed.data.backdrop,
			budget: parsed.data.budget,
			voteAverage: parsed.data.score ? parsed.data.score / 10 : undefined,
			voteCount: parsed.data.numberOfVotes,
			genre: {
				connect: parsed.data.genres.map((genreId) => ({ id: genreId })),
			},
			actors: {
				connect: parsed.data.actors.map((actorId) => ({ id: actorId })),
			},
		},
	});

	revalidatePath("/movies");
	return {
		success: {
			newMovieId: newMovie.id,
			status: true
		},
	};
}
