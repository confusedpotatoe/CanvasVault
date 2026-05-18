export interface ArtworkDto{
    id: string;
    titel: string;
    artist: string;
    year: number;
    collectionid: string;
}

export interface CreateArtworkCommand{
    title: string;
    artis: string;
    year: number;
    collectionId: string;
}