import { artworkDto } from "./artwork";

export interface CollectionDto{
    id: string;
    name: string;
    artwork?: artworkDto[];
}