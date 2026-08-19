import type { Metadata } from "next";
import "./globals.css";
import Image from "next/image";
import Link from "next/link";
import NavLinks from "@/components/NavLinks";
import { Suspense } from "react";
import { Toaster } from "@/components/ui/toast";

export const metadata: Metadata = {
  title: "Cinebase",
  description: "Best movie website out there!",
};

export default function RootLayout({ children }: LayoutProps<"/">) {
  return (
    <html lang="en" className={`h-full antialiased dark`}>
      <body className="min-h-full flex flex-col">
        <nav className="border-b-2 border-b-muted px-4 py-4">
          <div className="max-w-300 mx-auto flex items-center justify-between">
            <Link href="/">
              <Image
                className="w-17 h-15 object-contain"
                src="/icon.png"
                alt="cinebase logo"
                width={382}
                height={302}
              />
            </Link>
            <Suspense fallback={<div></div>}>
              <NavLinks />
            </Suspense>
          </div>
        </nav>
        <main className="flex-1 max-w-300 mx-auto w-full flex flex-col">
          {children}
        </main>
        <footer className="border-t-2 border-t-muted">
          <div className="max-w-300 mx-auto px-4 flex items-center justify-center gap-8 py-4">
            <a href="https://www.themoviedb.org/">
              <div className="w-15 h-15 flex items-center justify-center">
                <Image
                  src="/movieDbIcon.png"
                  alt="TMDB logo"
                  width={510}
                  height={400}
                />
              </div>
            </a>
            <p className="text-muted-foreground text-sm">
              Movies data and images, from TMDB (The Movie Database)
            </p>
          </div>
        </footer>
        <Toaster />
      </body>
    </html>
  );
}
