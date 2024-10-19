import type { Gender } from '../enum/gender.enum';

export interface NewInstructorInput {
  firstName: string;
  lastName: string;
  gender: Gender;
  email: string;
  password: string;
  confirmPassword: string;
  about?: string;
  countryCode: string;
  mobileNumber: string;
  license: string;
}

export interface VerifyCodeDto {
  email: string;
  token: string;
}
