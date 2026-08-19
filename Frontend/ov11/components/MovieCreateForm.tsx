"use client";

import { useActionState, useEffect, useRef } from "react";
import { Button } from "./ui/button";
import {
  Field,
  FieldGroup,
  FieldLabel,
  FieldDescription,
  FieldSeparator,
  FieldError,
} from "./ui/field";
import { Input } from "./ui/input";
import { Textarea } from "./ui/textarea";
import { createMovie } from "@/actions/createMovie";
import MultiSelectBox from "./MultiSelectBox";
import { toast } from "./ui/toast";
import { useRouter } from "next/navigation";

interface Props {
  genres: {
    id: number;
    name: string;
  }[];
  actors: {
    id: number;
    name: string;
  }[];
}

export default function MovieCreateForm({ actors, genres }: Props) {
  const router = useRouter();
  const [state, formAction, pending] = useActionState(createMovie, {});
  const handledId = useRef<string | null>(null);

  useEffect(() => {
    const id = state.success?.newMovieId;
    if (!id || handledId.current === id.toString()) return;

    handledId.current = id.toString();

    toast.add({
      title: "Movie created successfully",
      type: "success",
      timeout: 3000,
    });

    setTimeout(() => {
      router.push(`/movies/${id}`);
    }, 300);
  }, [state.success?.newMovieId, router]);

  return (
    <form
      action={formAction}
      className="max-w-2xl mx-auto p-4 border rounded-md shadow-md"
    >
      <FieldGroup>
        <Field>
          <FieldLabel htmlFor="title">Title *</FieldLabel>
          <Input
            id="title"
            name="title"
            type="text"
            placeholder="Movie Title"
            defaultValue={state.values?.title}
            aria-invalid={!!state.errors?.title}
            required
          />
          {state.errors?.title && <FieldError>{state.errors.title}</FieldError>}
        </Field>
        <Field>
          <FieldLabel htmlFor="tagline">Tagline *</FieldLabel>
          <Input
            id="tagline"
            name="tagline"
            defaultValue={state.values?.tagline}
            type="text"
            placeholder="Movie Tagline"
            aria-invalid={!!state.errors?.tagline}
            required
          />
          {state.errors?.tagline && (
            <FieldError>{state.errors.tagline}</FieldError>
          )}
        </Field>
        <Field>
          <FieldLabel htmlFor="description">Description *</FieldLabel>
          <Textarea
            id="description"
            name="description"
            placeholder="Movie Description"
            defaultValue={state.values?.description}
            required
            aria-invalid={!!state.errors?.description}
          />
          {state.errors?.description && (
            <FieldError>{state.errors.description}</FieldError>
          )}
        </Field>
        <Field>
          <FieldLabel htmlFor="releaseDate">Release Date *</FieldLabel>
          <Input
            id="releaseDate"
            name="releaseDate"
            type="date"
            defaultValue={state.values?.releaseDate}
            required
            aria-invalid={!!state.errors?.releaseDate}
          />
          {state.errors?.releaseDate && (
            <FieldError>{state.errors.releaseDate}</FieldError>
          )}
        </Field>
        <Field>
          <FieldLabel htmlFor="duration">Duration (minutes) *</FieldLabel>
          <Input
            id="duration"
            name="duration"
            type="number"
            placeholder="Movie Duration"
            defaultValue={state.values?.duration}
            aria-invalid={!!state.errors?.duration}
            required
          />
          {state.errors?.duration && (
            <FieldError>{state.errors.duration}</FieldError>
          )}
        </Field>
        <Field>
          <FieldLabel>Genre *</FieldLabel>
          <MultiSelectBox
            invalidInput={!!state.errors?.genres}
            inputName="genres"
            placeholder=""
            items={genres}
          />
          {state.errors?.genres && (
            <FieldError>{state.errors.genres}</FieldError>
          )}
        </Field>
        <Field>
          <FieldLabel>Actors *</FieldLabel>
          <MultiSelectBox
            invalidInput={!!state.errors?.actors}
            inputName="actors"
            placeholder=""
            items={actors}
          />
          {state.errors?.actors && (
            <FieldError>{state.errors.actors}</FieldError>
          )}
        </Field>
        <Field>
          <FieldLabel htmlFor="poster">Poster URL</FieldLabel>
          <Input
            id="poster"
            name="poster"
            type="url"
            placeholder="https://image.tmdb.org/....."
            defaultValue={state.values?.poster}
            aria-invalid={!!state.errors?.poster}
          />
          {state.errors?.poster ? (
            <FieldError>{state.errors.poster}</FieldError>
          ) : (
            <FieldDescription className="text-xs text-muted-foreground">
              (The image must come from the tmdb)
            </FieldDescription>
          )}
        </Field>
        <Field>
          <FieldLabel htmlFor="backdrop">Backdrop URL</FieldLabel>
          <Input
            id="backdrop"
            name="backdrop"
            type="url"
            placeholder="https://image.tmdb.org/....."
            defaultValue={state.values?.backdrop}
            aria-invalid={!!state.errors?.backdrop}
          />
          {state.errors?.backdrop ? (
            <FieldError>{state.errors.backdrop}</FieldError>
          ) : (
            <FieldDescription className="text-xs text-muted-foreground">
              (The image must come from the tmdb)
            </FieldDescription>
          )}
        </Field>
        <Field>
          <FieldLabel htmlFor="budget">Budget</FieldLabel>
          <Input
            id="budget"
            name="budget"
            type="number"
            min={1}
            placeholder="Movie Budget ($)"
            defaultValue={state.values?.budget}
            aria-invalid={!!state.errors?.budget}
          />
          {state.errors?.budget && (
            <FieldError>{state.errors.budget}</FieldError>
          )}
        </Field>
        <Field>
          <FieldLabel htmlFor="score">Score</FieldLabel>
          <Input
            id="score"
            name="score"
            type="number"
            placeholder="Movie Score (1-100)"
            min={1}
            max={100}
            defaultValue={state.values?.score}
            aria-invalid={!!state.errors?.score}
          />
          {state.errors?.score && <FieldError>{state.errors.score}</FieldError>}
        </Field>
        <Field>
          <FieldLabel htmlFor="numberOfVotes">Number of Votes</FieldLabel>
          <Input
            id="numberOfVotes"
            name="numberOfVotes"
            type="number"
            placeholder="Number of Votes"
            min={1}
            defaultValue={state.values?.numberOfVotes}
            aria-invalid={!!state.errors?.numberOfVotes}
          />
          {state.errors?.numberOfVotes && (
            <FieldError>{state.errors.numberOfVotes}</FieldError>
          )}
        </Field>
      </FieldGroup>
      <FieldSeparator className="my-4" />
      <Field className="mt-4">
        <Button disabled={pending} className="py-5" type="submit">
          {pending ? "⏳ Creating movie" : "Create Movie"}
        </Button>
      </Field>
    </form>
  );
}
