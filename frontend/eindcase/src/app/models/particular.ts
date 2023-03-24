import { Student } from './student';

export interface Particular extends Student {
  postalCode: string;
  place: string;
  IBAN: string;
}
