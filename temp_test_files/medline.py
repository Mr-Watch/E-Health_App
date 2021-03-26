from bs4 import BeautifulSoup
from requests import get
import json
import xmltodict

def assigner(data_entry, property_value):
    if data_entry['@name'] == (str)(property_value) : property_value = data_entry['#text']
    return property_value

search_argument = 'asthma'#input('Enter a search term ')
response = get('https://wsearch.nlm.nih.gov/ws/query?db=healthTopics&retmax=1&term='+search_argument)

dict_data = xmltodict.parse(response.text)


term = dict_data['nlmSearchResult']["term"]
url = dict_data['nlmSearchResult']['list']['document']['@url']
title = ''
for data_entires in dict_data['nlmSearchResult']['list']['document']['content']:
    title = assigner(data_entires,title)
    print(title)



json = '"term":"{}", "url":"{}", "title":{}, "altTitle":{}, "organizationName":"{}", "FullSummary":"{}", "groupName":"{}"'

print(title)

'''
article_processed = BeautifulSoup(article,'lxml').text

#print(dict['@name'])

with open('/home/sarantis/Desktop/test.json', 'w') as json_file:
    json.dump(dict, json_file)
    json_file.close()
    '''