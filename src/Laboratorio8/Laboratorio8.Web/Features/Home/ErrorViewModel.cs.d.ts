declare module Home.Server {
	interface errorViewModel {
		inviaSegnalazioneUrl: string;
		requestId: string;
		emailFrom: string;
		emailTo: string[];
		emailCC: string[];
		emailCCN: string[];
		subject: string;
		standardContent: string;
		content: string;
		emailOptions: any[];
		showRequestId: boolean;
	}
}
