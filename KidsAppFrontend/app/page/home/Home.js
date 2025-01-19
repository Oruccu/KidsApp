import { View, Text, SafeAreaView } from 'react-native';
import React from 'react';
import Background from '@/app/components/background';
import styles from './styles';
import Banner from '@/app/components/Banner';
import ListenToStory from '@/app/components/homeComponent/ListenToStory';
import { useNavigation } from '@react-navigation/native';
import { getAudioBookById } from '@/app/services/api';
import { playAudioFromUrl } from '@/app/utils/playAudioFromUrl';
import ContinueToListen from '@/app/components/homeComponent/ContinueToListen';
import Game from '@/app/components/homeComponent/Game';
import { stories } from '@/app/data/stories'; // Hikaye verilerini içe aktar

const Home = () => {
  const navigation = useNavigation();

  async function listenStory() {
    try {
      const audioBook = await getAudioBookById(1);
      if (audioBook?.audioFileUrl) {
        await playAudioFromUrl(audioBook.audioFileUrl);
      }
    } catch (error) {
      console.error(error);
    }
  }

  const navigateToStory = (storyId) => {
    navigation.navigate('Story', { storyId }); // Sadece storyId gönder
  };

  return (
    <Background showOverlay={true}>
      <SafeAreaView style={styles.container}>
        <Banner bannerTitle="Welcome to Storyland" />
        <View>
          <ContinueToListen onPress={listenStory} />
        </View>
        <View style={styles.storyContainer}>
          {Object.keys(stories).map((key) => (
            <ListenToStory
              key={key}
              Title={stories[key].title} // Başlık görünecek
              onPress={() => navigateToStory(key)} // Sadece id gönderiliyor
              url={
                key === 'theLittlePrince'
                  ? require('@/app/assets/icon/prince.png')
                  : require('@/app/assets/icon/snowWhite.png')
              }
            />
          ))}
        </View>
        <View>
          <Game />
        </View>
      </SafeAreaView>
    </Background>
  );
};

export default Home;
