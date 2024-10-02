import type { Gender } from '../enum/gender.enum';

export interface NewStudentInput {
  email: string;
  password: string;
}

export interface UpdateStudentInfo {
  firstName?: string;
  lastName?: string;
  gender?: Gender;
  dob?: string;
}
