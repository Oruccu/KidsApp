import { Audio } from 'expo-av';

export async function playAudioFromUrl(url) {
  try {
    console.log('Çalınacak URL:', url);
    const { sound } = await Audio.Sound.createAsync(
      { uri: url },
      { shouldPlay: true }
    );
    sound.setOnPlaybackStatusUpdate((status) => {
      if (status.didJustFinish) {
        console.log('Playback finished.');
      }
    });
  } catch (error) {
    console.error('Error playing audio:', error.message);
    console.error(error);
  }
}

