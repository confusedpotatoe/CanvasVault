import apiClient from './apiClient';
import { ArtworkDto, CreateArtworkCommand } from '../types/artwork';

export const artworkService = {
  getAll: async (): Promise<ArtworkDto[]> => {
    const response = await apiClient.get<ArtworkDto[]>('/artworks');
    return response.data; 
  },

  create: async (command: CreateArtworkCommand): Promise<ArtworkDto> => {
    const response = await apiClient.post<ArtworkDto>('/artworks', command);
    return response.data;
  },

  delete: async (id: string): Promise<void> => {
    await apiClient.delete(`/artworks/${id}`);
  }
};