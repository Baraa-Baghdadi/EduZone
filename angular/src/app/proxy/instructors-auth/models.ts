import type { Gender } from '../enum/gender.enum';

export interface NewInstructorInput {
  firstName: string;
  lastName: string;
  gender: Gender;
  email: string;
  password?: string;
  countryCode: string;
  mobileNumber: string;
  about?: string;
}

export interface VerifyCodeDto {
  email: string;
  token: string;
}
