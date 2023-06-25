export interface Feedback {
  id: string;
  title: string;
  description: string;
  rating: number;
  dateReviewed: string;
  userName: string;
}

export interface FeedbackFormValues {
  title: string;
  description: string;
  rating: number;
  dateReviewed: string;
}
